using App.Infrastructure.Repositories;
using App.Models.Entities;
using App.Services.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.Services.User.Commands;

public class CreateUserCommand : BaseUserCommand
{
}

public class CreateUserCommandHandler : BaseCommandHandler, IRequestHandler<CreateUserCommand, BaseResponse>
{
    private readonly IRepository<UserEntity> _userRepository;
    private readonly IValidator<CreateUserCommand> _validator;
    public CreateUserCommandHandler(
        IValidator<CreateUserCommand> validator, 
        IRepository<UserEntity> userRepository, 
        ILogger<CreateUserCommandHandler> logger) : base(logger)
    {
        ArgumentNullException.ThrowIfNull(nameof(userRepository));
        ArgumentNullException.ThrowIfNull(nameof(validator));

        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<BaseResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();
        var errors = new List<Error>();

        UserEntity? user = null;

        try
        {
            var validator = await _validator.ValidateAsync(request, cancellationToken).ConfigureAwait(false);
            if (!validator.IsValid)
            {
                validator.AddToErrorsModel(errors);
                response.Success = false;
                response.Errors = errors;

                return response;
            }

            user = new UserEntity()
            {
                Age = request.Age,
                BirthDate = request.BirthDate,
                Bmi = request.Bmi,
                Height = request.Height,
                Name = request.Name,
                Weight = request.Weight,
                DateCreated = DateTime.UtcNow
            };

            user = await _userRepository.Create(user, cancellationToken).ConfigureAwait(false);
            LogInformation("Create user success");

            response.Success = true;
            response.Message = "User successfully created!";
        }
        catch (Exception ex)
        {
            if(user is not null)
            {
                await _userRepository.Delete(user.UserId, cancellationToken).ConfigureAwait(false);
            }

            LogError(ex, "Error create user");

            response.Success = false;
            response.Message = "Error! Try again.";
        }

        return response;    
    }
}

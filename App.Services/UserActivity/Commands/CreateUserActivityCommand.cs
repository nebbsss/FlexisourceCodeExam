using App.Infrastructure.Repositories;
using App.Models.Entities;
using App.Services.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace App.Services.UserActivity.Commands;

public class CreateUserActivityCommand : BaseUserActivityCommand
{
}
public class CreateUserActivityCommandHandler : BaseCommandHandler, IRequestHandler<CreateUserActivityCommand, BaseResponse>
{
    private readonly IRepository<UserActivityEntity> _userActivityRepository;
    private readonly IValidator<CreateUserActivityCommand> _validator;
    public CreateUserActivityCommandHandler(
        IValidator<CreateUserActivityCommand> validator,
        IRepository<UserActivityEntity> userActivityRepository,
        ILogger<CreateUserActivityCommandHandler> logger) : base(logger)
    {
        ArgumentNullException.ThrowIfNull(nameof(userActivityRepository));
        ArgumentNullException.ThrowIfNull(nameof(validator));

        _userActivityRepository = userActivityRepository;
        _validator = validator;
    }

    public async Task<BaseResponse> Handle(CreateUserActivityCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseResponse();
        var errors = new List<Error>();

        UserActivityEntity? userActivity = null;

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

            userActivity = new UserActivityEntity()
            {
                UserId = request.UserId,
                AveragePace = request.AveragePace,
                Distance = request.Distance,
                Duration = request.Duration,
                EndDate = request.EndDate,
                StartedDate = request.StartedDate,
                DateCreated = DateTime.UtcNow
            };

            userActivity = await _userActivityRepository.Create(userActivity, cancellationToken).ConfigureAwait(false);
            LogInformation("Create user activity success");

            response.Success = true;
            response.Message = "User activity successfully created!";
        }
        catch (Exception ex)
        {
            if (userActivity is not null)
            {
                await _userActivityRepository.Delete(userActivity.UserActivityId, cancellationToken).ConfigureAwait(false);
            }

            LogError(ex, "Error create user activity");

            response.Success = false;
            response.Message = "Error! Try again.";
        }

        return response;
    }
}
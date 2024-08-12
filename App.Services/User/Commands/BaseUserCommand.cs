using MediatR;

namespace App.Services.User.Commands;

public class BaseUserCommand : IRequest<BaseResponse>
{
    public Guid UserId { get; set; }
    public string? Name { get; set; }
    public double? Weight { get; set; }
    public double? Height { get; set; }
    public DateTime? BirthDate { get; set; }
    public int? Age
    {
        get
        {
            if (!BirthDate.HasValue) return null;

            var today = DateTime.Today;
            var age = today.Year - BirthDate.Value.Year;

            return age > 0 ? age : 0;
        }
    }
    public double? Bmi
    {
        get
        {
            if(!Weight.HasValue || !Height.HasValue) return null;
            return Weight.Value / Math.Pow(Height.Value / 100.0, 2);
        }
    }
}

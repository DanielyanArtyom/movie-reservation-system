
namespace MovieReservation.Business.Model;

public class CheckAccessModel
{
    public required ResourceEnum TargetPage { get; set; }
    public required AccessTypesEnum Permission { get; set; }
}
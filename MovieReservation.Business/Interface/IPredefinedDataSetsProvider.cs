namespace MovieReservation.Business.Interface;

public interface IPredefinedDataSetsProvider
{
    List<string> GetPaginationOptions();
    List<string> GetSortingOptions();
    List<string> GetPublishDateOptions();
    List<string> GetAllPermissions();
}
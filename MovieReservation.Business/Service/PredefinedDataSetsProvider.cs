namespace MovieReservation.Business.Service;

public class PredefinedDataSetsProvider: IPredefinedDataSetsProvider
{
    public List<string> GetPaginationOptions()
    {
        return EnumExtension.GetEnumDisplayNames<PaginationOption>();
    }

    public List<string> GetSortingOptions()
    {
        return EnumExtension.GetEnumDisplayNames<SortingOption>();
    }

    public List<string> GetPublishDateOptions()
    {
        return EnumExtension.GetEnumDisplayNames<PublishDateOption>();
    }

    public List<string> GetAllPermissions()
    {
        return EnumExtension.GetEnumDisplayNames<AccessTypesEnum>();
    }
}
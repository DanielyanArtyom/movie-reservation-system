namespace MovieReservation.Business.Enum;

public enum SortingOption
{
    [Display(Name = "Most Popular")]
    MostPopular,
    
    [Display(Name = "Most Commented")]
    MostCommented,
    
    [Display(Name = "Price ASC")]
    PriceAsc,
    
    [Display(Name = "Price DESC")]
    PriceDesc,
    
    [Display(Name = "New")]
    New
}
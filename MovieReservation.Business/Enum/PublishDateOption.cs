namespace MovieReservation.Business.Enum;

public enum PublishDateOption
{
    [Display(Name = "Last Week")]
    LastWeek,
    
    [Display(Name = "Last Month")]
    LastMonth,
    
    [Display(Name = "Last Year")]
    LastYear,
    
    [Display(Name = "Two Years")]
    TwoYears,
    
    [Display(Name = "Three Years")]
    ThreeYears
}
namespace MovieReservation.Business.Enum;

public enum PaginationOption
{
    [Display(Name = "10")]
    Ten = 10,
    
    [Display(Name = "20")]
    Twenty = 20,
    
    [Display(Name = "50")]
    Fifty = 50,
    
    [Display(Name = "100")]
    OneHundred = 100,
    
    [Display(Name = "All")]
    All = int.MaxValue,
}
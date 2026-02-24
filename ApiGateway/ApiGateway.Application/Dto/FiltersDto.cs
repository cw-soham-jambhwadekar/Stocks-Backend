using System.ComponentModel.DataAnnotations;

public class FiltersDto
{
    [RegularExpression(@"^(\d+(\s\d+)*)?$", 
        ErrorMessage = "Fuel must be space-separated numeric IDs.")]
    public string? Fuel { get; set; }

    [RegularExpression(@"^\d+-\d+$", 
        ErrorMessage = "Budget must be in format min-max.")]
    public string? Budget { get; set; }

    [RegularExpression(@"^(\d+(\s\d+)*)?$", 
        ErrorMessage = "Cars must be space-separated numeric IDs.")]
    public string? Cars { get; set; }

    [RegularExpression(@"^\d+$", 
        ErrorMessage = "City must be a numeric value")]
    [Range(1, int.MaxValue, 
        ErrorMessage = "City must be a positive number.")]
    public int? City { get; set; }

    public int? So { get; set; }
    public int? Sc { get; set; }

    [Range(0, int.MaxValue, 
        ErrorMessage = "Page number must be non-negative")]
    public int Pn { get; set; }
}

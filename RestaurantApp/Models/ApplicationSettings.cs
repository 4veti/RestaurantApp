namespace RestaurantApp.Models;

public class ApplicationSettings
{
    public RunMode RunMode { get; set; }
    public string BaseAPIUrl { get; set; } = string.Empty;
}

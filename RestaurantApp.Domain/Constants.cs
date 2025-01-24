namespace RestaurantApp.Domain;

public static class Constants
{
    public const int FoodNameMinLength = 2;
    public const int FoodNameMaxLength = 60;

    public const int NetGramsMin = 10;
    public const int NetGramsMax = 1500;

    public const int OrderNameMinLength = 2;
    public const int OrderNameMaxLength = 50;

    public const int MinMillilitres = 20;
    public const int MaxMillilitres = 2000;

    public const double MinAlcoholPercentage = 0;
    public const double MaxAlcoholPercentage = 65;

    public const int MinDrinkNameLength = 2;
    public const int MaxDrinkNameLength = 60;

    public const int MinDrinkTypeNameLength = 2;
    public const int MaxDrinkTypeNameLength = 60;

    public const double MinPrice = 0.1;
    public const double MaxPrice = 350;
}

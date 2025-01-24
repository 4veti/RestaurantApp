namespace RestaurantApp.Domain;

public static class ErrorMessages
{
    private const string GenericOutOfRange = " must be between {0} and {1}!";

    public const string DefaultError = "An unexpected error occured.";

    public const string FailedToInsert = "ERROR: Failed to insert object with name {0} in the database.";
    public const string FailedToDelete = "ERROR: Failed to delete object with name {0}.";

    public const string FoodNameAlreadyExists = "ERROR: Food with name \"{0}\" already exists!";
    public const string NetGramsOutOfRange = "ERROR: Net grams!" + GenericOutOfRange;
    public const string PriceOutOfRange = "ERROR: Price" + GenericOutOfRange;
    public const string InvalidFoodTypeId = "ERROR: Food type with Id {0} does not exist!";
    public const string DrinkNameAlreadyExists = "ERROR: Drink with name \"{0}\" already exists!";
    public const string InvalidDrinkTypeId = "ERROR: Drink type with Id {0} does not exist!";
    public const string MillilitresOutOfRange = "ERROR: Millilitres" + GenericOutOfRange;
    public const string AlcoholPercentageOutOfRange = "ERROR: Alcohol percentage" + GenericOutOfRange;
}

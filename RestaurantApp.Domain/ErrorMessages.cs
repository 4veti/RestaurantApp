namespace RestaurantApp.Domain;

public static class ErrorMessages
{
    private const string GenericOutOfRange = " must be between {0} and {1}!";

    public const string DefaultError = "An unexpected error occured.";
    public const string UnexpectedValidationError = "An unexpected error occured during data validation.";
    public const string UnexpectedDatabaseError = "An unexpected error occured during a database call.";

    public const string FailedToInsert = "ERROR: Failed to insert {0}.";
    public const string FailedToUpdate = "ERROR: Failed to update {0}.";
    public const string FailedToDelete = "ERROR: Failed to delete {0}.";

    public const string InvalidNameLength = "ERROR: Name" + GenericOutOfRange;
    public const string InvalidFoodTypeId = "ERROR: Food type with Id {0} does not exist!";
    public const string InvalidDrinkTypeId = "ERROR: Drink type with Id {0} does not exist!";
    public const string InvalidFoodIDs = "ERROR: No Food(s) exist with ID(s) [{0}]!";
    public const string InvalidDrinkIDs = "ERROR: No Drink(s) exist with ID(s) [{0}]!";

    public const string NetGramsOutOfRange = "ERROR: Net grams!" + GenericOutOfRange;
    public const string PriceOutOfRange = "ERROR: Price" + GenericOutOfRange;
    public const string MillilitresOutOfRange = "ERROR: Millilitres" + GenericOutOfRange;
    public const string AlcoholPercentageOutOfRange = "ERROR: Alcohol percentage" + GenericOutOfRange;
    public const string NameCannotBeNullOrEmpty = "ERROR: Name cannot be null or an empty string!";
    public const string FoodNameAlreadyExists = "ERROR: Food with name \"{0}\" already exists!";
    public const string DrinkNameAlreadyExists = "ERROR: Drink with name \"{0}\" already exists!";
    public const string IdMustBeAboveZero = "ERROR: Id must be a positive number above zero!";
    public const string NotAllOrderItemsWereInserted = "ERROR: Not all order items were inserted for order with ID: {}";
    public const string AlcoholByVolumeIsMandatory = "ERROR: Alcoholic percentage is mandatory when the drink is alcoholic!";
    public const string AlcoholByVolumeIsOnlyForAlcoholicDrinks = "ERROR: Alcoholic percentage is only for alcoholic drinks!";

}

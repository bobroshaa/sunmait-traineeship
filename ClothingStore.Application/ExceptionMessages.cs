namespace ClothingStore.Application;

public static class ExceptionMessages
{
    public static readonly string BrandNotFound = "Brand with id {0} does not exist.";
    public static readonly string ProductNotFound = "Product with id {0} does not exist.";
    public static readonly string OrderNotFound = "Order with id {0} does not exist.";
    public static readonly string OrderItemNotFound = "Order item with id {0} does not exist";
    public static readonly string CategoryNotFound = "Category with id {0} does not exist.";
    public static readonly string ProductQuantityIsNotAvailable = "Quantity of product with id {0} is not available. Available quantity {1}.";
    public static readonly string SectionNotFound = "Section with id {0} does not exist.";
    public static readonly string BrandAlreadyExists = "Brand with name {0} already exists.";
    public static readonly string UserNotFound = "User with id {0} does not exist.";
    public static readonly string ReviewNotFound = "Review with id {0} does not exist.";
    public static readonly string CategoryLinked = "Category with id {0} already linked to section with id {1}.";
    public static readonly string CategoryNotLinked = "Category with id {0} isn't linked to section with id {1}.";
    public static readonly string EmailIsNotUnique = "Email {0} is already occupied.";
    public static readonly string PhoneNumberIsNotUnique = "Phone number {0} is already occupied.";
    public static readonly string SectionCategoryNotFound = "The relation with id {0} between category and section does not exist.";
}
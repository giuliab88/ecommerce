namespace SampleCommerce.Common
{
    public static class ErrorMessages
    {
        public const string EmailAlreadyExists = "This email address is already associated with an account.";
        public const string UserNotFound = "User not found.";
        public const string AddressNotFound  = "Address not found.";
        public const string OrderNotFound = "Order not found.";
        public const string ProductNotFound = "Product not found.";
        public const string ReviewNotFound = "Review not found.";
        public const string SKUsNotFound = "Product_Details not found.";
        public const string DatabaseError = "An internal error occurred while saving to the database.";
        public const string UpdateNotPersisted = "No changes were made.";
        public const string InvalidName = "The username cannot contain numbers.";
        public const string MissingIVA = "IVA is mandatory for seller users.";
        public const string MissingTradingName = "Trading name is mandatory for sellers users.";
        public const string IvaSizeViolation = "IVA must have 11 digits.";
        public const string InsufficientStock = "We don't have this amount of this Item.";
        public const string InvalidQuantity = "Forbbiden: The quantity of one product in this order is negative.";
        public const string InvalidPrice = "Forbbiden: The price of one product in this order is negative.";
        public const string InvalidProductDetails = "A product need at least a SKU (price/stock).";
        public const string InvalidResetToken = "Il token non è valido o è scaduto.";
        public const string InvalidConfirmToken = "Il token di conferma non è valido.";
        public const string PasswordTooShort = "La password deve essere di almeno 8 caratteri.";
    }
}

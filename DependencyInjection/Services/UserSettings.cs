namespace DependencyInjection.Services;

public class UserSettings
{
    // Kurzschreibweise fuer ReadOnly Eigenschaft
    public string DefaultPaymentMethod => $"{GetType().Name}\t{GetHashCode()} CreditCard";

    public string DefaultPaymentMethodAlt
    {
        get
        {
            return $"{GetType().Name}\t{GetHashCode()}\tCreditCard";
        }
    }
}
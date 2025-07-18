namespace DependencyInjection.Services;

public class ShoppingService : IShoppingService
{
    private readonly IPaymentService _paymentService;
    private readonly UserSettings _userSettings;

    // Klassischer Weg vor 20 Jahren oder so :)
    public PaymentService paymentServiceLegacy = new PaymentService();

    public ShoppingService(IPaymentService paymentService, UserSettings userSettings)
    {
        _paymentService = paymentService;
        _userSettings = userSettings;
    }

    public void AddOrder(string product)
    {
        Console.WriteLine($"{GetType().Name}\t\tOrder for {product} added");
    }

    public void PayOrder()
    {
        // Nicht die beste Lösung
        //paymentServiceLegacy.MakePayment();

        // Bessere Loesung, weil wir hier eine Abstraktion verwenden
        Console.WriteLine($"{GetType().Name}\t{GetHashCode()}\tPay order");
        Console.WriteLine(_userSettings.DefaultPaymentMethod);

        _paymentService.MakePayment();
    }
}

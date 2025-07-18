namespace DependencyInjection.Services;

public class PaymentService : IPaymentService
{
    public void MakePayment()
    {
        Console.WriteLine($"{GetType().Name}\t{GetHashCode()}\tPayment made");
    }
}

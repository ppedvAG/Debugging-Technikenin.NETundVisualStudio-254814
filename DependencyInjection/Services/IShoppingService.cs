namespace DependencyInjection.Services
{
    public interface IShoppingService
    {
        void AddOrder(string product);
        void PayOrder();
    }
}
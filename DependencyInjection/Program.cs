using DependencyInjection.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                // Abhaengigkeit von aussen injizieren
                // DIP: Dependency Inversion Principle (https://de.wikipedia.org/wiki/Dependency-Inversion-Prinzip)
                var paymentService = new PaymentService();

                // Die konsumierende Klasse soll nicht wissen, welche Implementierung verwendet wird (Abstraktion)
                var shoppingService = new ShoppingService(paymentService, new UserSettings());
                shoppingService.PayOrder();
            }

            Console.WriteLine("\nUse Dependency Injection");
            // Registrierung der Typen erfolgt immer beim Start der Anwendung
            ServiceProvider serviceProvider = RegisterServicesOnStartupOnce();

            // Aufloesung erfolgt bei der ersten Verwendung der Abhaengigkeit
            {
                // PaymentService wird vom DependencyInjection-Package automatisch erzeugt
                // UserSettings werden einmalig erzeugt und immer die gleiche Instanz verwendet
                var shoppingService = serviceProvider.GetService<IShoppingService>();
                shoppingService.AddOrder("Laptop");
                shoppingService.PayOrder();

                Console.WriteLine();
                // Hier wird ein neuer Payment Service erzeugt
                var shoppingService2 = serviceProvider.GetService<IShoppingService>();
                shoppingService2.AddOrder("Tablet");
                shoppingService2.PayOrder();
            }


            Console.WriteLine("\nUse Scopes");
            using (var scope = serviceProvider.CreateScope())
            {
                // Hier wird der Payment Service einmalig fuer diesen Scope erzeugt
                var shoppingService = scope.ServiceProvider.GetService<IShoppingService>();
                shoppingService.AddOrder("Hund");
                shoppingService.PayOrder();

                Console.WriteLine();
                // Hier wird immer der gleiche Payment Service benutzt
                var shoppingService2 = scope.ServiceProvider.GetService<IShoppingService>();
                shoppingService2.AddOrder("Katze");
                shoppingService2.PayOrder();


                // Innerhalb von using wird am ende Dispose() aufgerufen (vgl. DisposePattern)
                //scope.Dispose();
            }


            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private static ServiceProvider RegisterServicesOnStartupOnce()
        {

            // Install-Package Microsoft.Extensions.DependencyInjection
            // https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?tabs=aspnetcore2x
            var services = new ServiceCollection();

            // Aufruf einer statischen Methode
            SetupServicesExtension.AddShoppingServices(services);

            // Aufruf als Extension-Method
            services.AddShoppingServices();

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}

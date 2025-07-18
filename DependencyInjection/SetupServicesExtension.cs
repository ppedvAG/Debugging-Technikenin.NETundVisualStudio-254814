using DependencyInjection.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
    // Eine Extension Klasse muss statisch sein
    public static class SetupServicesExtension
    {
        // Eine Extension Methode muss statisch sein
        // und der erste Parameter der Methode muss das "this" Keyword haben
        public static void AddShoppingServices(this IServiceCollection services)
        {
            // Transient ist das Standardszenario: Beim Aufloesen der Abhaengigkeit wird immer eine neue Instanz erzeugt
            services.AddTransient<IShoppingService, ShoppingService>();

            // Bei Scoped wird jedes mal eine neue Instanz innerhalb eines Scopes erzeugt (bei Web Anwendungen je Request)
            services.AddScoped<IPaymentService, PaymentService>();

            // Eine Instanz wird nur einmal erzeugt fuer den gesamten Lebenszyklus der Anwendung (verhaelt sich wie static)
            // Kann sehr problematisch bei Datenbankzugriff oder Dateizugriff sein, weil Verbindung nie geschlossen wird
            // Singleton Design Pattern https://refactoring.guru/design-patterns/singleton
            services.AddSingleton<UserSettings>();
        }
    }
}

namespace HelloDebug.Data
{
    // ISP: Interface Segregation Principle bedeutet, Faehigkeiten sollen in Interfaces aufgeteilt werden
    public class Bird : Creature, IVolatile
    {
        public void Fly()
        {
            Console.WriteLine($"{Name} can fly!");
        }
    }
}

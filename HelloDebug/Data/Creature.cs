namespace HelloDebug.Data
{
    public class Creature : ICreature
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public void Talk()
        {
            Console.WriteLine($"{Name} says hello!");
        }
    }
}

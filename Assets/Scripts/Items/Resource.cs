namespace Items
{
    public class Resource : Item
    {
        public int amount;
        public Resource(string name, string desc, int amount)
        {
            this.name = name;
            this.desc = desc;
            this.amount = amount;
        }
    }
}

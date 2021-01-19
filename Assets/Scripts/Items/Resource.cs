namespace Items
{
    public class Resource : Item
    {
        public int amount;
        public Resource(string itemName, string desc, int amount)
        {
            this.itemName = itemName;
            this.desc = desc;
            this.amount = amount;
        }
    }
}

namespace CSVHandler
{
    public class DisplayNameAttribute : Attribute
    {
        public DisplayNameAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }
    }
}
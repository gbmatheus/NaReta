namespace NaReta.Domain.Entities
{
    public class Category
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = string.Empty;

        public Category(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(ResourceErrorMessages.NAME_EMPTY_OR_NULL);

            Name = name;
        }
    }
}

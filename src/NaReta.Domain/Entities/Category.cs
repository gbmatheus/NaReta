namespace NaReta.Domain.Entities
{
    public class Category
    {
        public string Title { get; private set; } = string.Empty;

        public Category(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException(ResourceErrorMessages.TITLE_EMPTY_OR_NULL);

            Title = title;
        }
    }
}

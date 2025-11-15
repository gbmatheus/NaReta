using NaReta.Common;
namespace NaReta.Domain.Entities;

public class Category
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;

    public Category() { }

    public Category(string name)
    {
        Name = name;
        Validate();
    }

    public void ChangeName(string name)
    {
        Name = name;
        Validate();
    }

    private void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new ArgumentException(ResourceErrorMessages.NAME_EMPTY_OR_NULL);
    }
}

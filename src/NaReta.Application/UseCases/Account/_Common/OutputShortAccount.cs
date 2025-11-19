using System.Text.Json.Serialization;

namespace NaReta.Application.UseCases.Account._Common;

public class OutputShortAccount
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Balance { get; set; }
}

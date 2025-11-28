using System.ComponentModel.DataAnnotations;

namespace NaReta.Infra.Configuration;

public class DatabaseSettings
{
    public const string SECTION_NAME = "ConnectionStrings";

    [Required]
    [MinLength(10, ErrorMessage = "Connection string to short")]
    public string DatabaseConnection { get; set; } = string.Empty;
}

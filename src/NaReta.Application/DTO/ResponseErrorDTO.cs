namespace NaReta.Application.DTO;

public class ResponseErrorDTO
{
    public List<string> ErrorMessages { get; set; }

    public ResponseErrorDTO(string errorMessage)
    {
        ErrorMessages = new List<string> { errorMessage };
    }

    public ResponseErrorDTO(List<string> errorMessages)
    {
        ErrorMessages = errorMessages;
    }
}

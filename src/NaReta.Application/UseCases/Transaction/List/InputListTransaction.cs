using NaReta.Application.DTO;

namespace NaReta.Application.UseCases.Transaction.List;

public class InputListTransaction : QueryStringParametersDTO
{
    public int AccountId { get; set; }
    public DateTime? StartDate { get; set; } = null;
    public DateTime? EndDate { get; set; } = null;
}

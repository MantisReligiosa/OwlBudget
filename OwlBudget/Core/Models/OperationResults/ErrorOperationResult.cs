namespace Core.Models.OperationResults;

public record ErrorOperationResult : OperationResult
{
    public override bool IsValid => false;
    public string ErrorMessage { get; init; }
}
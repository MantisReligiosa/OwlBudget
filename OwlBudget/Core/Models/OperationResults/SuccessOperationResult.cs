namespace Core.Models.OperationResults;

public record SuccessOperationResult : OperationResult
{
    public override bool IsValid => true;
}
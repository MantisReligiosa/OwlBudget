namespace Core.Models.OperationResults;

public abstract record OperationResult
{
    public abstract bool IsValid { get; }
}
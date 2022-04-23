using Core.Models.OperationResults;

namespace OwlBudget.Models;

public class OperationResponse
{
    public OperationResponse(OperationResult operationResult)
    {
        IsSuccess = operationResult.IsValid;
        if (operationResult is ErrorOperationResult errorOperationResult)
            ErrorMessage = errorOperationResult.ErrorMessage;
    }

    public bool IsSuccess { get; }
    public string ErrorMessage { get; }
}
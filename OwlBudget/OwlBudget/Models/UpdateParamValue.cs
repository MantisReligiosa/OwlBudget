namespace OwlBudget.Models;

public class UpdateParamValue<TObjectId, TValue>
{
    public TObjectId ObjectId { get; set; }
    public TValue Value { get; set; }
}
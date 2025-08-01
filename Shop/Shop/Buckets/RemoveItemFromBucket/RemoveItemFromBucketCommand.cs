namespace Shop.Processing.RemoveItemFromBucket;

public class RemoveItemFromBucketCommand
{
    public Guid BucketId { get; }
    public Guid ItemId { get; }
    public string RemovedBy { get; }

    public RemoveItemFromBucketCommand(Guid bucketId, Guid itemId, string removedBy)
    {
        BucketId = bucketId;
        ItemId = itemId;
        RemovedBy = removedBy;
    }
}

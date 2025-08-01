namespace Shop.Buckets.AddItemToBucket;

public class AddItemToBucketCommand
{
    public Guid BucketId { get; }
    public Guid ItemId { get; }
    public string AddedBy { get; }

    public AddItemToBucketCommand(Guid bucketId, Guid itemId, string addedBy)
    {
        BucketId = bucketId;
        ItemId = itemId;
        AddedBy = addedBy;
    }
}


namespace Shop.Buckets.GetBucketItemsStatistics;

public class BucketItemsStaticsticsQueryResponse
{
    public Dictionary<Pair, int> Pair = new Dictionary<Pair, int>();
    public int TotalItemsInBuckets { get; set; }
    public int TotalBuckets { get; set; }
}

public class Pair
{
    public required Guid BucketId { get; set; }
    public required Guid ItemId { get; set; }
}


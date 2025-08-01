namespace Shop.Buckets.GetBucket;

public class GetBucketQueryResponse
{
    public Guid Id { get; init; }
    public Dictionary<Item, int> Items { get; init; } = new Dictionary<Item, int>();
    public decimal Price => Items.Sum(entry => entry.Key.Price * entry.Value);

    public GetBucketQueryResponse(Guid id, Dictionary<Item, int> items)
    {
        Id = id;
        Items = items;
    }
}

namespace Shop.Buckets;

public class Bucket
{
    public required Guid Id { get; init; }
    public Dictionary<Item, int> Items { get; init; } = new Dictionary<Item, int>();
    public decimal Price => Items.Sum(entry => entry.Key.Price * entry.Value);
    public required DateTimeOffset LastChangedAtUtc { get; set; }
    public required string LastChangedBy { get; set; }

    public void CanAddItem()
    {
        if (Items.Count >= 5)
            throw new InvalidOperationException("Cannot add more than 5 items to bucket.");
    }

    public void AddItem(Item item)
    {
        if (Items.ContainsKey(item))
            Items[item] += 1;
        else
            Items.Add(item, 1);
    }

    public void RemoveItem(Item item)
    {
        if (Items.ContainsKey(item))
            Items[item] -= 1;
        else
            throw new InvalidOperationException("Item is not currently in bucket");
    }

    public void ChangeBucketDetails(string lastChangedBy)
    {
        LastChangedBy = lastChangedBy;
        LastChangedAtUtc = DateTimeOffset.UtcNow;
    }
}
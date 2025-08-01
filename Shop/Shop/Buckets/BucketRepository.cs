namespace Shop.Buckets;

public class BucketRepository : IBucketRepository
{
    public async Task<Bucket> Get(Guid id)
    {

        if (id == Guid.Empty)
        {
            throw new ArgumentException("...", nameof(id));
        }

        await Task.CompletedTask;

        return new Bucket
        {
            Id = id,
            Items = [],
            LastChangedAtUtc = DateTimeOffset.UtcNow,
            LastChangedBy = "author"
        };
    }

    public async Task Save(Bucket bucket)
    {
        // Stub implementation - would be database call
        await Task.CompletedTask;
    }

    public async Task<IEnumerable<Bucket>> GetAll()
    {
        var items = await GetAllItems();
        List<Item> itemList = items.ToList();
        return new List<Bucket>
        {
            new Bucket
            {
                Id = Guid.NewGuid(),
                Items = new Dictionary<Item, int>
                {
                    { itemList[0], 5 },
                    { itemList[1], 1 },
                },
                LastChangedAtUtc = DateTimeOffset.UtcNow,
                LastChangedBy = "author"
            },
            new Bucket
            {
                Id = Guid.NewGuid(),
                Items = new Dictionary<Item, int>
                {
                    { itemList[1], 2 },
                    { itemList[2], 1 },
                },
                LastChangedAtUtc = DateTimeOffset.UtcNow,
                LastChangedBy = "author"
            },
            new Bucket
            {
                Id = Guid.NewGuid(),
                Items = new Dictionary<Item, int>
                {
                    { itemList[2], 2 },
                    { itemList[0], 1 },
                },
                LastChangedAtUtc = DateTimeOffset.UtcNow,
                LastChangedBy = "author"
            }
        };
    }

    public async Task<IEnumerable<Item>> GetAllItems()
    {
        await Task.CompletedTask;

        List<Item> items = new List<Item>()
    {
        new Item()
        {
            Id = Guid.NewGuid(),
            Name = "Flower",
            Price = 100,
        },
        new Item()
        {
            Id = Guid.NewGuid(),
            Name = "Apple",
            Price = 15,
        },
        new Item()
        {
            Id = Guid.NewGuid(),
            Name = "Banana",
            Price = 20,
        }
    };

        return items;
    }
}

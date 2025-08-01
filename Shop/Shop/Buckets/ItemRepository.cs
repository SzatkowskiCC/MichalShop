namespace Shop.Buckets;

public class ItemRepository : IItemRepository
{
    public async Task<Item> Get(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("...", nameof(id));
        }

        await Task.Delay(100);

        Item item = new Item()
        {
            Id = Guid.NewGuid(),
            Name = "Flower",
            Price = 100,
        };

        return item;
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

    public async Task Save(Item item)
    {
        await Task.CompletedTask;
    }
}

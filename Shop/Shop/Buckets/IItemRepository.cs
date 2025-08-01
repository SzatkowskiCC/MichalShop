namespace Shop.Buckets;

public interface IItemRepository
{
    public Task<Item> Get(Guid id);
    public Task<IEnumerable<Item>> GetAllItems();
    public Task Save(Item item);
}


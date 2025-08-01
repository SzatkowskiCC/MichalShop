using Shop.Helpers;

namespace Shop.Buckets.AddItemToBucket;

public class AddItemToBucketCommandHandler : ICommandHandler<AddItemToBucketCommand>
{
    private readonly IBucketRepository _bucketRepository;
    private readonly IItemRepository _itemRepository;

    public AddItemToBucketCommandHandler(IBucketRepository bucketRepository, IItemRepository itemRepository)
    {
        _bucketRepository = bucketRepository;
        _itemRepository = itemRepository;
    }

    public async Task Handle(AddItemToBucketCommand command)
    {
        Bucket bucket = await _bucketRepository.Get(command.BucketId);

        bucket.CanAddItem();

        Item item = await _itemRepository.Get(command.ItemId);

        bucket.AddItem(item);

        bucket.ChangeBucketDetails(command.AddedBy);

        await _bucketRepository.Save(bucket);
    }
}


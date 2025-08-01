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
        if (command.BucketId == Guid.Empty || command.ItemId == Guid.Empty)
        {
            throw new ArgumentException("Invalid ID: One or both of the IDs are empty.");
        }

        if (command.AddedBy == null)
        {
            throw new ArgumentException("The 'AddedBy' field cannot be null.");
        }

        Bucket bucket = await _bucketRepository.Get(command.BucketId);

        if (bucket == null)
        {
            throw new KeyNotFoundException($"Bucket with ID {command.BucketId} does not exist.");
        }

        var item = await _itemRepository.Get(command.ItemId);

        bucket.CanAddItem();

        if (item == null)
        {
            throw new KeyNotFoundException($"Item with ID {command.ItemId} does not exist.");
        }

        bucket.AddItem(item);

        bucket.ChangeBucketDetails(command.AddedBy);

        await _bucketRepository.Save(bucket);
    }
}


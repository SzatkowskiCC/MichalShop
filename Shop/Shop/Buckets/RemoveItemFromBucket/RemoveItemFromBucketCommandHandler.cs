using Shop.Helpers;
using Shop.Processing.RemoveItemFromBucket;

namespace Shop.Buckets.RemoveItemFromBucket;

public class RemoveItemFromBucketCommandHandler : ICommandHandler<RemoveItemFromBucketCommand>
{
    private readonly IBucketRepository _bucketRepository;
    private readonly IItemRepository _itemRepository;

    public RemoveItemFromBucketCommandHandler(IBucketRepository bucketRepository, IItemRepository itemRepository)
    {
        _bucketRepository = bucketRepository;
        _itemRepository = itemRepository;
    }

    public async Task Handle(RemoveItemFromBucketCommand command)
    {
        if (command.BucketId == Guid.Empty || command.ItemId == Guid.Empty)
        {
            throw new ArgumentException("Invalid ID: One or both of the IDs are empty.");
        }

        if (command.RemovedBy == null)
        {
            throw new ArgumentException("The 'RemovedBy' field cannot be null.");
        }

        Bucket bucket = await _bucketRepository.Get(command.BucketId);

        if (bucket == null)
        {
            throw new KeyNotFoundException($"Bucket with ID {command.BucketId} does not exist.");
        }

        var item = await _itemRepository.Get(command.ItemId);

        if (item == null)
        {
            throw new KeyNotFoundException($"Item with ID {command.ItemId} does not exist.");
        }

        bucket.RemoveItem(item);

        bucket.ChangeBucketDetails(command.RemovedBy);

        await _bucketRepository.Save(bucket);
    }
}

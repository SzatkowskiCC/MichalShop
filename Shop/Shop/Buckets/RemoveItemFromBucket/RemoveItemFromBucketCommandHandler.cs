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
        Bucket bucket = await _bucketRepository.Get(command.BucketId);

        var item = await _itemRepository.Get(command.ItemId);

        bucket.RemoveItem(item);

        bucket.ChangeBucketDetails(command.RemovedBy);

        await _bucketRepository.Save(bucket);
    }
}

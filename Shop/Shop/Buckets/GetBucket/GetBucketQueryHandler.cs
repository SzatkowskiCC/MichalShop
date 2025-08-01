using Shop.Helpers;

namespace Shop.Buckets.GetBucket;

public class GetBucketQueryHandler : IQueryHandler<GetBucketQuery, GetBucketQueryResponse>
{
    private readonly IBucketRepository _bucketRepository;
    public GetBucketQueryHandler(IBucketRepository bucketRepository)
    {
        _bucketRepository = bucketRepository;
    }

    public async Task<GetBucketQueryResponse> Handle(GetBucketQuery query)
    {
        if (query.BucketId == Guid.Empty)
        {
            throw new ArgumentException("Invalid bucket ID.");
        }

        Bucket bucket = await _bucketRepository.Get(query.BucketId);

        if (bucket == null)
        {
            throw new KeyNotFoundException($"Bucket with ID {query.BucketId} does not exist.");
        }

        await Task.CompletedTask;

        return new GetBucketQueryResponse(bucket.Id, bucket.Items);
    }
}

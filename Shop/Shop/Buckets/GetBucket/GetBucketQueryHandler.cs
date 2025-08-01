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
        Bucket bucket = await _bucketRepository.Get(query.BucketId);

        await Task.CompletedTask;

        return new GetBucketQueryResponse(bucket.Id, bucket.Items);
    }
}

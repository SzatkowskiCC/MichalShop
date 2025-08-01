using Shop.Helpers;

namespace Shop.Buckets.GetBucket;

public class GetBucketQuery : IQuery<GetBucketQueryResponse>
{
    public Guid BucketId { get; }

    public GetBucketQuery(Guid bucketId)
    {
        BucketId = bucketId;
    }
}

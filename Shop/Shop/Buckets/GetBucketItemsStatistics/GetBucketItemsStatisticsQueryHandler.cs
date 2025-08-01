using Shop.Helpers;

namespace Shop.Buckets.GetBucketItemsStatistics;

public class GetBucketItemsStatisticsQueryHandler : IQueryHandler<GetBucketItemsStatisticsQuery, BucketItemsStaticsticsQueryResponse>
{
    private readonly IBucketRepository _bucketRepository;

    public GetBucketItemsStatisticsQueryHandler(IBucketRepository bucketRepository)
    {
        _bucketRepository = bucketRepository;
    }

    public async Task<BucketItemsStaticsticsQueryResponse> Handle(GetBucketItemsStatisticsQuery getBucketItemsStatisticsQuery)
    {
        IEnumerable<Bucket> buckets = await _bucketRepository.GetAll();


        if (buckets == null)
        {
            throw new InvalidOperationException("Failed to retrieve buckets from the repository.");
        }

        BucketItemsStaticsticsQueryResponse bucketItemsStaticsticsResponse = new BucketItemsStaticsticsQueryResponse();

        bucketItemsStaticsticsResponse.TotalBuckets = buckets.Count();

        foreach (var bucket in buckets)
        {
            foreach (var itemPair in bucket.Items)
            {
                var pair = new Pair
                {
                    BucketId = bucket.Id,
                    ItemId = itemPair.Key.Id
                };

                bucketItemsStaticsticsResponse.TotalItemsInBuckets += itemPair.Value;

                if (bucketItemsStaticsticsResponse.Pair.ContainsKey(pair))
                {
                    bucketItemsStaticsticsResponse.Pair[pair] += itemPair.Value;
                }
                else
                {
                    bucketItemsStaticsticsResponse.Pair.Add(pair, itemPair.Value);
                }
            }
        }

        return bucketItemsStaticsticsResponse;
    }
}

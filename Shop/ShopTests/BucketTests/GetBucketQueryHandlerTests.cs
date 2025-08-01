using Moq;

using Shop.Buckets;
using Shop.Buckets.GetBucket;

namespace ShopTests.BucketTests;

public class GetBucketQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnCorrectResponse()
    {
        var mockBucketRepository = new Mock<IBucketRepository>();
        var bucketId = Guid.NewGuid();

        var bucket = new Bucket
        {
            Id = bucketId,
            Items = new Dictionary<Item, int>
            {
                { new Item { Id = Guid.NewGuid(), Name = "Item1", Price = 10m }, 3 },
                { new Item { Id = Guid.NewGuid(), Name = "Item2", Price = 20m }, 2 }
            },
            LastChangedAtUtc = DateTimeOffset.UtcNow,
            LastChangedBy = "User"
        };

        mockBucketRepository.Setup(repo => repo.Get(bucketId)).ReturnsAsync(bucket);

        var handler = new GetBucketQueryHandler(mockBucketRepository.Object);

        var query = new GetBucketQuery(bucketId);
        var result = await handler.Handle(query);


        Assert.IsType<GetBucketQueryResponse>(result);
        Assert.NotNull(result);
        Assert.Equal(bucketId, result.Id);
        Assert.Equal(2, result.Items.Count);
        Assert.Contains(result.Items, item => item.Key.Name == "Item1" && item.Value == 3);
        Assert.Contains(result.Items, item => item.Key.Name == "Item2" && item.Value == 2);
    }
}

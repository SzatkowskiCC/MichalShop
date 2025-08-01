using Moq;

using Shop.Buckets;
using Shop.Buckets.GetBucketItemsStatistics;

namespace ShopTests.BucketTests;

public class GetBucketItemsStatisticsQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldReturnStatistics()
    {
        var mockBucketRepository = new Mock<IBucketRepository>();

        var buckets = new List<Bucket>
        {
            new Bucket
            {
                Id = Guid.NewGuid(),
                Items = new Dictionary<Item, int>
                {
                    { new Item { Id = Guid.NewGuid(), Name = "Item1", Price = 10m }, 3 },
                    { new Item { Id = Guid.NewGuid(), Name = "Item2", Price = 20m }, 2 }
                },
                LastChangedAtUtc = DateTimeOffset.UtcNow,
                LastChangedBy = "User"
            },
            new Bucket
            {
                Id = Guid.NewGuid(),
                Items = new Dictionary<Item, int>
                {
                    { new Item { Id = Guid.NewGuid(), Name = "Item1", Price = 10m }, 2 },
                    { new Item { Id = Guid.NewGuid(), Name = "Item3", Price = 30m }, 4 }
                },
                LastChangedAtUtc = DateTimeOffset.UtcNow,
                LastChangedBy = "User"
            }
        };

        mockBucketRepository.Setup(repo => repo.GetAll()).ReturnsAsync(buckets);

        var handler = new GetBucketItemsStatisticsQueryHandler(mockBucketRepository.Object);

        var query = new GetBucketItemsStatisticsQuery();
        var result = await handler.Handle(query);

        Assert.NotNull(result);
        Assert.IsType<BucketItemsStaticsticsQueryResponse>(result);
        Assert.Equal(4, result.Pair.Count);
        Assert.Equal(11, result.TotalItemsInBuckets);
        Assert.Equal(2, result.TotalBuckets);
    }
}

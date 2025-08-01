using Shop.Buckets;

namespace ShopTests.BucketTests;

public class BucketTests
{
    [Fact]
    public void CanAddItem_ShouldThrowInvalidOperationException_WhenItemsCountIsFiveOrMore()
    {
        Bucket bucket = new Bucket
        {
            Id = Guid.NewGuid(),
            LastChangedAtUtc = DateTimeOffset.UtcNow,
            LastChangedBy = "TestUser"
        };

        Item[] items =
        [
        new Item { Id = Guid.NewGuid(), Name = "Item1", Price = 10m },
        new Item { Id = Guid.NewGuid(), Name = "Item2", Price = 20m },
        new Item { Id = Guid.NewGuid(), Name = "Item3", Price = 30m },
        new Item { Id = Guid.NewGuid(), Name = "Item4", Price = 40m },
        new Item { Id = Guid.NewGuid(), Name = "Item5", Price = 50m }
    ];

        foreach (Item item in items)
        {
            bucket.AddItem(item);
        }

        Exception exception = Assert.Throws<InvalidOperationException>(() => bucket.CanAddItem());
        Assert.Equal("Cannot add more than 5 items to bucket.", exception.Message);
    }

    [Fact]
    public void CanAddItem_ShouldNotThrowException_WhenItemsCountIsLessThanFive()
    {
        Bucket bucket = new Bucket
        {
            Id = Guid.NewGuid(),
            LastChangedAtUtc = DateTimeOffset.UtcNow,
            LastChangedBy = "TestUser"
        };

        Item item = new Item { Id = Guid.NewGuid(), Name = "Item1", Price = 10m };

        bucket.AddItem(item);

        Exception exception = Record.Exception(() => bucket.CanAddItem());

        Assert.Null(exception);
    }

    [Fact]
    public void AddItem_ShouldIncreaseQuantity_WhenItemIsAdded()
    {
        Bucket bucket = new Bucket
        {
            Id = Guid.NewGuid(),
            LastChangedAtUtc = DateTimeOffset.UtcNow,
            LastChangedBy = "TestUser"
        };

        Item item = new Item { Id = Guid.NewGuid(), Name = "Item1", Price = 10m };

        bucket.AddItem(item);

        Assert.Single(bucket.Items);
        Assert.Equal(1, bucket.Items[item]);
    }

    [Fact]
    public void AddItem_ShouldIncreaseQuantity_WhenItemAlreadyExistsInBucket()
    {
        Bucket bucket = new Bucket
        {
            Id = Guid.NewGuid(),
            LastChangedAtUtc = DateTimeOffset.UtcNow,
            LastChangedBy = "TestUser"
        };

        Item item = new Item { Id = Guid.NewGuid(), Name = "Item1", Price = 10m };

        bucket.AddItem(item);
        bucket.AddItem(item);

        Assert.Single(bucket.Items);
        Assert.Equal(2, bucket.Items[item]);
    }

    [Fact]
    public void RemoveItem_ShouldDecreaseQuantity_WhenItemIsRemoved()
    {
        Bucket bucket = new Bucket
        {
            Id = Guid.NewGuid(),
            LastChangedAtUtc = DateTimeOffset.UtcNow,
            LastChangedBy = "TestUser"
        };

        Item item = new Item { Id = Guid.NewGuid(), Name = "Item1", Price = 10m };

        bucket.AddItem(item);
        bucket.AddItem(item);

        bucket.RemoveItem(item);

        Assert.Single(bucket.Items);
        Assert.Equal(1, bucket.Items[item]);
    }

    [Fact]
    public void RemoveItem_ShouldThrowInvalidOperationException_WhenItemIsNotInBucket()
    {
        Bucket bucket = new Bucket
        {
            Id = Guid.NewGuid(),
            LastChangedAtUtc = DateTimeOffset.UtcNow,
            LastChangedBy = "TestUser"
        };

        Item item = new Item { Id = Guid.NewGuid(), Name = "Item1", Price = 10m };

        Exception exception = Assert.Throws<InvalidOperationException>(() => bucket.RemoveItem(item));
        Assert.Equal("Item is not currently in bucket", exception.Message);
    }

    [Fact]
    public void Price_ShouldReturnCorrectTotalPrice()
    {
        Bucket bucket = new Bucket
        {
            Id = Guid.NewGuid(),
            LastChangedAtUtc = DateTimeOffset.UtcNow,
            LastChangedBy = "TestUser"
        };

        Item item1 = new Item { Id = Guid.NewGuid(), Name = "Item1", Price = 10m };
        Item item2 = new Item { Id = Guid.NewGuid(), Name = "Item2", Price = 20m };

        bucket.AddItem(item1);
        bucket.AddItem(item1);
        bucket.AddItem(item2);

        decimal totalPrice = bucket.Price;

        Assert.Equal(40m, totalPrice);
    }

    [Fact]
    public void ChangeBucketDetails_ShouldChangeDetails()
    {
        Bucket bucket = new Bucket
        {
            Id = Guid.NewGuid(),
            LastChangedAtUtc = DateTimeOffset.UtcNow.AddHours(-1),
            LastChangedBy = "System"
        };

        DateTimeOffset dateTimeOffset = bucket.LastChangedAtUtc;

        Item item1 = new Item { Id = Guid.NewGuid(), Name = "Item1", Price = 10m };

        bucket.AddItem(item1);

        bucket.ChangeBucketDetails("TestUser");

        Assert.Equal("TestUser", bucket.LastChangedBy);
        Assert.NotEqual(dateTimeOffset, bucket.LastChangedAtUtc);

    }
}
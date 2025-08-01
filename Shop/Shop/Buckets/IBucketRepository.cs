namespace Shop.Buckets;

public interface IBucketRepository
{
    Task<Bucket> Get(Guid id);
    Task Save(Bucket bucket);
    Task<IEnumerable<Bucket>> GetAll();
}

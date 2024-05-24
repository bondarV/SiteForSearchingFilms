using MovieSearch.DataAccess.Data;
using MovieSearch.DataAccess.Repository.IRepository;
using MovieSearch.Model;

namespace MovieSearch.DataAccess.Repository;
    
public class ReviewRepository : Repository<Review>, IReviewRepository
{
    private ApplicationDbContext _db;
    public ReviewRepository(ApplicationDbContext db) :base(db)
    {
        _db = db;
    }
    public void Update(Review obj)
    {
        _db.Reviews.Update(obj);
    }
}
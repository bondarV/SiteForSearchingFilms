using MovieSearch;
using System.Collections.Generic;
using MovieSearch.Model;

namespace MovieSearch.DataAccess.Repository.IRepository;

public interface IReviewRepository : IRepository<Review>
{
    void Update(Review obj);
}
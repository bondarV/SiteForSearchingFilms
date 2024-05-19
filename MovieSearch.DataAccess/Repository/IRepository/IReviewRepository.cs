using MovieSearch;
using MovieSearch.Model.Models;
using System.Collections.Generic;
namespace MovieSearch.DataAccess.Repository.IRepository;

public interface IReviewRepository : IRepository<Review>
{
    void Update(Review obj);
}
using MovieSearch;
using MovieSearch.Model.Models;
using System.Collections.Generic;
namespace MovieSearch.DataAccess.Repository.IRepository;

public interface IGenreRepository : IRepository<Genre>
{
    void Update(Genre obj);
    
}
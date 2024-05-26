using System.Collections;
using MovieSearch;
using System.Collections.Generic;
using MovieSearch.Model;

namespace MovieSearch.DataAccess.Repository.IRepository;

public interface IGenreRepository : IRepository<Genre>
{
    void Update(Genre obj);
    
}
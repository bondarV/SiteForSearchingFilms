using MovieSearch.Model.Models;

namespace MovieSearch.DataAccess.Repository.IRepository;

public interface IFilmRepository : IRepository<Film>
{
    void Update(Film? obj);
}
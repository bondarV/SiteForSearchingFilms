using MovieSearch.Model.Models;

namespace MovieSearch.DataAccess.Repository.IRepository;

public interface IFilmGenreRepository : IRepository<FilmGenre>
{
    void Update(FilmGenre obj);   
}
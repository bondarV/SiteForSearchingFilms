using MovieSearch.DataAccess.Data;
using MovieSearch.DataAccess.Repository.IRepository;
using MovieSearch.Model;

namespace MovieSearch.DataAccess.Repository;

public class FilmGenreRepository : Repository<FilmGenre>, IFilmGenreRepository
{
    private ApplicationDbContext _db;

    public FilmGenreRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }   

    public void Update(FilmGenre obj)
    {
        _db.FilmGenres.Update(obj);
    }
}
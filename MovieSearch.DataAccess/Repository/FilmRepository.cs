using MovieSearch.DataAccess.Data;
using MovieSearch.DataAccess.Repository.IRepository;
using MovieSearch.Model;

namespace MovieSearch.DataAccess.Repository;

public class FilmRepository :  Repository<Film>, IFilmRepository
{
    
    private ApplicationDbContext _db;

    public FilmRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }   

    public void Update(Film? obj)
    {
        if (obj != null) _db.Films.Update(obj);
    }
    

    
}
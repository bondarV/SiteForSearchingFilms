using MovieSearch.DataAccess.Data;
using MovieSearch.DataAccess.Repository.IRepository;
using MovieSearch.Model;

namespace MovieSearch.DataAccess.Repository
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        private ApplicationDbContext _db;

        public GenreRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }   

        public void Update(Genre obj)
        {
            _db.Genres.Update(obj);
        }
        
        
    }
}
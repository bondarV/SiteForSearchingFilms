using MovieSearch.DataAccess.Data;
using MovieSearch.DataAccess.Repository.IRepository;

using System.Collections.Generic;

namespace MovieSearch.DataAccess.Repository;

public class UnitOfWork : IUnitOfWork
{
    private ApplicationDbContext _db;
    public IReviewRepository Review { get;private set; }
    public IGenreRepository Genre { get;private set; }
    public IFilmRepository Film { get;private set; }
    public IFilmGenreRepository FilmGenre { get;private set; }
    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Review = new ReviewRepository(_db);
        Genre = new GenreRepository(_db);
        Film = new FilmRepository(_db);
        FilmGenre = new FilmGenreRepository(_db);
    }
    
    public void Save()
    {
        _db.SaveChanges();
    }
}
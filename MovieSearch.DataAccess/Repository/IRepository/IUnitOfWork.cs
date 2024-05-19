using System.Collections.Generic;
namespace MovieSearch.DataAccess.Repository.IRepository;

public interface IUnitOfWork
{
    IReviewRepository Review { get; }     
    IGenreRepository Genre { get; }
    IFilmRepository Film { get; }
    
    IFilmGenreRepository FilmGenre { get; }

    
    void Save();
}
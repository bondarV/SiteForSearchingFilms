using System.Linq.Expressions;
using MovieSearch.Model.Models;

namespace MovieSearch.DataAccess.Repository.IRepository;

    public interface IRepository<T> where T : class 
    {
        //T-Category
        IEnumerable<T> GetAll();
        T Get(Expression<Func<T, bool>> filter);

        void Add(T entity);

        //I don't use Update because i want update exactly only one object 
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);


    }
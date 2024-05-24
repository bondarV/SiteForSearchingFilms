using System.Linq.Expressions;

namespace MovieSearch.DataAccess.Repository.IRepository;

    public interface IRepository<T> where T : class 
    {
        //T-Category
        IEnumerable<T> GetAll(string? includeProperties = null);
        T Get(Expression<Func<T, bool>> filter,string? includeProperties = null);

        void Add(T entity);

        //I don't use Update because i want update exactly only one object 
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);


    }
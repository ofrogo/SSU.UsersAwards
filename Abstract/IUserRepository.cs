using System.Collections.Generic;

namespace Abstract
{
    public interface IRepository<T>
    {
        T Get(int id);
        void Add(T t);
        void Delete(T t);
        List<T> GetAll();
    }
}
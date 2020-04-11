using System.Collections.Generic;

namespace BLL.Interfaces
{
    public interface IService<T>
    {
        T Get(int id);
        int Add(T t);
        int Delete(T t);
        List<T> GetAll();
    }
}
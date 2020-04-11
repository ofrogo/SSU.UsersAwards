using System.Collections.Generic;
using Model;

namespace DAL.Interfaces
{
    public interface IUserRepository
    {
        User Get(int id);
        void Add(User user);
        void Delete(User user);
        List<User> GetAll();
    }
}
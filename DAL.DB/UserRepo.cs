using System.Collections.Generic;
using DAL.File.Interfaces;
using Model;

namespace DAL.DB
{
    public class UserRepo: IRepository<User>
    {
        public User Get(int id)
        {
            throw new System.NotImplementedException();
        }

        public void Add(User user)
        {
            using var a = new UsersAndAwardsContext();
            a.Users.Add(user);
            a.SaveChanges();
        }

        public void Delete(User t)
        {
            throw new System.NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new System.NotImplementedException();
        }
    }
}
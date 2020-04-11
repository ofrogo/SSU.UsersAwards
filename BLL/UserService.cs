using System;
using System.Collections.Generic;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using Model;


namespace BLL
{
    public class UserService : IService<User>
    {
        private readonly IUserRepository _repository;

        public UserService(TypeDataBase type)
        {
            switch (type)
            {
                case TypeDataBase.File:
                    _repository = new UserJsonRepo();
                    break;
                case TypeDataBase.RelationalDb:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Illegal type of data base.");
            }
        }

        public User Get(int id)
        {
            return _repository.Get(id);
        }

        public int Add(User user)
        {
            try
            {
                _repository.Add(user);
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        public int Delete(User user)
        {
            try
            {
                _repository.Delete(user);
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        public List<User> GetAll()
        {
            return _repository.GetAll();
        }
    }
}
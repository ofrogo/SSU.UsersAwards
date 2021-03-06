﻿using System;
using System.Collections.Generic;
using Abstract;
using DAL.File;
using Model;


namespace BLL
{
    public class UserService : IService<User>
    {
        private readonly IRepository<User> _repositoryUser;
        private readonly IRepository<Award> _repositoryAward;
        private readonly IRepository<UserAward> _usersAwardsRepo;

        public UserService(TypeDataBase type)
        {
            switch (type)
            {
                case TypeDataBase.File:
                    _repositoryUser = new UserJsonRepo();
                    _repositoryAward = new AwardJsonRepo();
                    _usersAwardsRepo = new UsersAwardsJsonRepo();
                    break;
                case TypeDataBase.RelationalDb:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Illegal type of data base.");
            }
        }

        public User Get(int id)
        {
            var user = _repositoryUser.Get(id);
            user.Awards = _usersAwardsRepo.GetAll().FindAll(ua => ua.IdUser == user.Id)
                .ConvertAll(ua => _repositoryAward.Get(ua.IdAward));
            return user;
        }

        public int Add(User user)
        {
            try
            {
                _repositoryUser.Add(user);
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
                _repositoryUser.Delete(user);
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        public List<User> GetAll()
        {
            return _repositoryUser.GetAll();
        }
    }
}
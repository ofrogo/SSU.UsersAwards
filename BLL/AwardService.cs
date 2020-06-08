using System;
using System.Collections.Generic;
using Abstract;
using DAL.File;
using Model;

namespace BLL
{
    public class AwardService:IService<Award>
    {
        private readonly IRepository<User> _repositoryUser;
        private readonly IRepository<Award> _repositoryAward;
        private readonly IRepository<UserAward> _usersAwardsRepo;

        public AwardService(TypeDataBase type)
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

        public Award Get(int id)
        {
            var award = _repositoryAward.Get(id);
            award.Users = _usersAwardsRepo.GetAll().FindAll(ua => ua.IdAward == id)
                .ConvertAll(ua => _repositoryUser.Get(ua.IdUser));
            return award;
        }

        public int Add(Award t)
        {
            try
            {
                _repositoryAward.Add(t);
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        public int Delete(Award t)
        {
            try
            {
                _repositoryAward.Delete(t);
                return 1;
            }
            catch
            {
                return -1;
            }
        }

        public List<Award> GetAll()
        {
            return _repositoryAward.GetAll();
        }
    }
}
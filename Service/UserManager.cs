using System.Collections.Generic;
using Core.Utilities.Results;
using Infrastructure;
using Repository;

namespace Service
{
    public class UserManager : IUserService
    {
        IUserRepository _userDal;

        public UserManager(IUserRepository userDal)
        {
            _userDal = userDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public User GetByMail(string email)
        {
            return _userDal.GetByMail(email);
        }
        
        public DataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(),"Data Listed");
        }
    }
}

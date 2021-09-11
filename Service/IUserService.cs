using System.Collections.Generic;
using Core.Utilities.Results;
using Infrastructure;

namespace Service
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        void Add(User user);
        DataResult<List<User>> GetAll();
        User GetByMail(string email);

    }
}
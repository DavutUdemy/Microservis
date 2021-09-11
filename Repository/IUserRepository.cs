using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Infrastructure;

namespace Repository
{
    public interface IUserRepository
    {
        void Add(User user);
        Task<bool> Update(User user);
        List<User> GetAll();
        Task<User> GetById(int id);
        User  GetByMail(string email);

        Task<bool> Delete(int id);
        List<OperationClaim> GetClaims(User user);
    }
}
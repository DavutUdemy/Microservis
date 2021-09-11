using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Repository.DB;

namespace Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly OnionDBContext _context;
        public UserRepository(OnionDBContext context)
        {
            _context = context;
        }
        
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new OnionDBContext())
            {
                var result = from operationClaim in context.OperationClaims
                    join userOperationClaim in context.UserOperationClaims
                        on operationClaim.Id equals userOperationClaim.OperationClaimId
                    where userOperationClaim.UserId == user.Id
                    select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();

            }
        }

        public void Add(User user)
        {
            
                 _context.User.AddAsync(user);
                 _context.SaveChangesAsync();
            
           
        }

        public User GetByMail(string email)
        {
            var result =  _context.User.Where(e => e.Email == email).FirstOrDefault();
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var result = await _context.User.Where(e => e.Id == id).FirstOrDefaultAsync();
                if (result != null)
                {
                    _context.User.Remove(result);
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<User> GetAll()
        {
            var result =  _context.User.ToList();
            return result;
        }

        public async Task<User> GetById(int id)
        {
            var result = await _context.User.Where(e => e.Id == id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> Update(User user)
        {
            try
            {
                _context.User.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }

        }
    }
}
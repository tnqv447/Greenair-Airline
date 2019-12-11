using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
namespace ApplicationCore.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> getAccountByPersonId(string person_id);
        Task<IEnumerable<Account>> getAvailableAccount();
        Task<IEnumerable<Account>> getDisabledAccount();


        Task disable(string username);
        Task activate(string username);
        Task disable(Account acc);
        Task activate(Account acc);
    }
}
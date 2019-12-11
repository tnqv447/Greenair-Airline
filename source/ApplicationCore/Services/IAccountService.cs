using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.DTOs;
namespace ApplicationCore.Services
{
    public interface IAccountService : IService<Account, AccountDTO, AccountDTO>
    {
        //query
        Task<IEnumerable<AccountDTO>> getAllAccountAsync();
        Task<AccountDTO> getAccountAsync(string username);
        Task<AccountDTO> getAccountByPersonIdAsync(string person_id);

        Task<IEnumerable<AccountDTO>> getAvailableAccountAsync();
        Task<IEnumerable<AccountDTO>> getDisabledAccountAsync();


        //action
        Task addAccountAsync(AccountDTO dto);
        Task removeAccountAsync(string person_id);
        Task updateAccountAsync(AccountDTO dto);

        Task<bool> isExistedUsernameAsync(string username);
        Task<bool> loginCheckAsync(AccountDTO dto);
        Task<Person> getPersonByAccount(string username);

        Task disableAccountAsync(string person_id);
        Task activateAccountAsync(string person_id);

    }
}
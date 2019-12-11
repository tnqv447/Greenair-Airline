using System.Linq;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Globalization;
using System;
using System.Threading.Tasks;
using ApplicationCore;
using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;
using LinqKit;
using System.Collections.Generic;
namespace Infrastructure.Persistence.Repos
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        protected new GreenairContext Context => base.Context as GreenairContext;
        public AccountRepository(GreenairContext context) : base(context)
        {

        }

        public async Task<Account> getAccountByPersonId(string person_id)
        {
            var predicate = PredicateBuilder.New<Account>();
            predicate = predicate.And(m => m.PersonId.Equals(person_id));
            var acc = await this.FindAsync(predicate);
            if (acc.Count() != 1) return null;
            return acc.ElementAt(0);
        }

        private async Task<IEnumerable<Account>> getAccountsByStatus(STATUS status)
        {
            return await this.FindAsync(m => m.Status == status);
        }

        public async Task<IEnumerable<Account>> getAvailableAccount()
        {
            return await this.getAccountsByStatus(STATUS.AVAILABLE);
        }

        public async Task<IEnumerable<Account>> getDisabledAccount()
        {
            return await this.getAccountsByStatus(STATUS.DISABLED);
        }

        private async Task changeAccountStatus(string username, STATUS status)
        {
            try
            {
                var acc = await this.GetByAsync(username);
                acc.Status = status;
                this.Context.Update(acc);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("ChangeAccountStatus() Unexpected: " + e);
            }
        }
        private async Task changeAccountStatus(Account acc, STATUS status)
        {
            try
            {
                acc.Status = status;
                this.Context.Update(acc);
                await this.Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("ChangeAccountStatus() Unexpected: " + e);
            }
        }

        public async Task activate(string username)
        {
            await this.changeAccountStatus(username, STATUS.AVAILABLE);
        }

        public async Task disable(string username)
        {
            await this.changeAccountStatus(username, STATUS.DISABLED);
        }

        public async Task activate(Account acc)
        {
            await this.changeAccountStatus(acc, STATUS.AVAILABLE);
        }

        public async Task disable(Account acc)
        {
            await this.changeAccountStatus(acc, STATUS.DISABLED);
        }
    }
}
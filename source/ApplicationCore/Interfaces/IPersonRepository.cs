using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task disable(string person_id);
        Task activate(string person_id);
    }
}
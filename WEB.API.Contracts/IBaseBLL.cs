using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEB.API.Entities.Models;

namespace WEB.API.Contracts
{
    public interface IBaseBLL: IDisposable
    {
        Task<Employee> Get(Guid id);
        Task<IEnumerable<Employee>> GetAll();
        Task Create(Employee employee);
        Task Update(Guid id, Employee employee);
        Task Delete(Employee employee);
    }
}

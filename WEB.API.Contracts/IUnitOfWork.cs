using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEB.API.Entities.Models;

namespace WEB.API.Contracts
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Employee> Employee { get; }
        Task SaveAsyn();
    }
}

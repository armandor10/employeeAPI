using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEB.API.Contracts;
using WEB.API.Entities.Models;
using WEB.API.Repositories;

namespace WEB.API.BLL
{
    public class EmployeeBLL: IBaseBLL
    {
        private readonly IUnitOfWork unitOfWork;

        public EmployeeBLL(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Employee> Get(Guid id)
        {
            return await unitOfWork.Employee.GetByIDAsync(id);
        }

        public async Task Create(Employee employee)
        {
            unitOfWork.Employee.Insert(employee);
            await unitOfWork.SaveAsyn();
        }

        public async Task<IEnumerable<Employee>> GetAll()
        {
            return await unitOfWork.Employee.GetAsync();
        }

        public async Task Update(Guid id, Employee employee)
        {
            unitOfWork.Employee.Update(employee);
            try
            {
                await unitOfWork.SaveAsyn();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task Delete(Employee employee)
        {
            unitOfWork.Employee.Delete(employee);
            try
            {
                await unitOfWork.SaveAsyn();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    unitOfWork.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}

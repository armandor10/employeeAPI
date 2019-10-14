using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WEB.API.Contracts;
using WEB.API.Entities.Models;
using WEB.API.DAL.Data;

namespace WEB.API.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private JustInTimeContext _dbContext;
        private BaseRepository<Employee> _employee;

        public UnitOfWork(JustInTimeContext _dbContext)
        {
            this._dbContext = _dbContext;
        }
        public IRepository<Employee> Employee {
            get
            {
                return _employee ??
                    (_employee = new BaseRepository<Employee>(_dbContext));
            }
        }

        public async Task SaveAsyn()
        {
            await _dbContext.SaveChangesAsync();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
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

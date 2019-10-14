using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WEB.API.Entities.Models;

namespace WEB.API.DAL.Data
{
    public class JustInTimeContext: DbContext
    {
        public JustInTimeContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Employee> Employee { get; set; }
    }
}

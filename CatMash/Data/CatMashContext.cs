using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatMash.Models;

namespace CatMash.Data
{
    public class CatMashContext : DbContext
    {
        public CatMashContext(DbContextOptions<CatMashContext> options) : base(options)
        {

        }
        public DbSet<Cat> Cat { get; set; }
    }
}

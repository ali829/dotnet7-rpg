using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
    public class RpgContext : DbContext
    {
        public RpgContext(DbContextOptions<RpgContext> options) : base(options)
        {

        }
        public DbSet<Character> Characters => Set<Character>();
    }
}
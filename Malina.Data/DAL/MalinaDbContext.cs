using Malina.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Malina.Data.DAL
{
    public class MalinaDbContext : IdentityDbContext<User>
    {
        public MalinaDbContext(DbContextOptions<MalinaDbContext> options) : base(options)
        {

        }

        public DbSet<Slider> Sliders { get; set; }
    }
}

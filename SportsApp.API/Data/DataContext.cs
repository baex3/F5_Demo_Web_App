using Microsoft.EntityFrameworkCore;
using SportsApp.API.Models;
using System.Linq;

namespace SportsApp.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options){}
        
        public DbSet<Value> Values { get; set; }

        public DbSet<User> Users { get; set; }
    
    }
}
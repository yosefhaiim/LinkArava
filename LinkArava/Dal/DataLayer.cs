using LinkArava.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkArava.Dal
{
    public class DataLayer: DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<PostModel> Posts { get; set; }

        public DataLayer(DbContextOptions<DataLayer> option): base(option) { Database.EnsureCreated(); }
    }
}

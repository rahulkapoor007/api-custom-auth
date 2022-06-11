using AuthorizationAndAuthentication.Repositories.Context.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthorizationAndAuthentication.Repositories.Context.Context
{
    public class SqlContext : DbContext
    {
        private IConfiguration _config;
        public SqlContext(
            DbContextOptions<SqlContext> options,
            IConfiguration config)
            : base(options)
        {
            _config = config;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserModel>().HasNoKey();
            modelBuilder.Entity<UserListModel>().HasNoKey();
            modelBuilder.Entity<SPCommonResponse>().HasNoKey();
            modelBuilder.Entity<SPCommonResponseWithIdentity<string>>().HasNoKey();
            modelBuilder.Entity<SPCommonResponseWithIdentity<int>>().HasNoKey();

            base.OnModelCreating(modelBuilder);

        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UserListModel> UsersList { get; set; }
        public DbSet<SPCommonResponse> SpResponse { get; set; }
        public DbSet<SPCommonResponseWithIdentity<string>> SpResponseString { get; set; }
        public DbSet<SPCommonResponseWithIdentity<int>> SpResponseInteger { get; set; }
    }
}

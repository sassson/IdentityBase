namespace IdentityBase.Public.EntityFramework.DbContexts
{
    using System;
    using System.Threading.Tasks;
    using IdentityBase.Public.EntityFramework.Entities;
    using IdentityBase.Public.EntityFramework.Extensions;
    using IdentityBase.Public.EntityFramework.Interfaces;
    using IdentityBase.Public.EntityFramework.Configuration;
    using Microsoft.EntityFrameworkCore;

    public class UserAccountDbContext : DbContext, IUserAccountDbContext
    {
        private readonly EntityFrameworkOptions _options;

        public UserAccountDbContext(
            DbContextOptions<UserAccountDbContext> dbContextOptions,
            EntityFrameworkOptions options)
            : base(dbContextOptions)
        {
            this._options = options ??
                throw new ArgumentNullException(nameof(this._options));
        }

        public DbSet<UserAccount> UserAccounts { get; set; }

        public DbSet<ExternalAccount> ExternalAccounts { get; set; }

        public DbSet<UserAccountClaim> UserAccountClaims { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureUserAccountContext(this._options);

            base.OnModelCreating(modelBuilder);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using N5Company.Domain.Entities;

namespace N5Company.Infrastructure.Persistence
{
    public class PermissionsDbContext : DbContext
    {
        public PermissionsDbContext(DbContextOptions<PermissionsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>()
                .HasOne(p => p.PermissionType)
                .WithMany(pt => pt.Permissions)
                .HasForeignKey(p => p.PermissionTypeId);

            base.OnModelCreating(modelBuilder);
        }
    }
}

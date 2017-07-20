using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FamilyPhotosWithIdentity.Models;
using FamilyPhotosWithIdentity.Models.Github;
using AutoMapper;
using FamilyPhotosWithIdentity.Automapper;

namespace FamilyPhotosWithIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            Mapper.Initialize(cfg=>cfg.AddProfile<GithubProfile>());
        }

        public DbSet<GithubRequest> GithubRequests { get; set; }
        public DbSet<Hook> Hooks { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Milestone> Milestones { get; set; }
        public DbSet<Repository> Repositories { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<User> GithubUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public void AddOrUpdate<T>(T entity)
            where T : class, IEntityWithID
        {
            if (entity == default(T))
            {
                return;
            }

            var savedEntity = Set<T>().SingleOrDefault(x => x.id == entity.id);
            if (savedEntity == null)
            {
                Add(entity);
            }
            else
            {
                Mapper.Map(entity, savedEntity);
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MSToDoDB.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSToDoDB
{
    public class MSToDoContext: DbContext
    {
        public string ConnectionString { get; private set; }

        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Capability> Capabilities { get; set; }
        public DbSet<FolderImportMetadata> FolderImportMetadatas { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<LinkedEntity> LinkedEntities { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskFolder> TaskFolders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }

        public MSToDoContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public static string CreateConnectionString(string dbPath)
        {
            return $"Data Source={dbPath}";
        }
    }
}

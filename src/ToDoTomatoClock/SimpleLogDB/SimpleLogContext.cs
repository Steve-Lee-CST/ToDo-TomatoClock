using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using SimpleLogDB.Modules;

namespace SimpleLogDB
{
    public class SimpleLogContext : DbContext
    {
        public DbSet<SimpleGroup> SimpleGroups { get; set; }
        public DbSet<SimpleTaskFolder> SimpleTaskFolders { get; set; }
        public DbSet<SimpleTask> SimpleTasks { get; set; }
        public DbSet<TomatoClockLog> TomatoClockLogs { get; set; }
        public DbSet<InterruptionLog> InterruptionLogs { get; set; }

        public string ConnectionString { get; private set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }

        public SimpleLogContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public static string CreateConnectionString(string dbPath)
        {
            return $"Data Source={dbPath}";
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using AssetSecretary.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssetSecretary.Db
{
    public class DbConnectionEntity : DbContext
    {
        public DbSet<DailyPrice> DailyPrice { get; set; }
        private string DbPath { get; set; }

        public DbConnectionEntity()
        {
            DbPath = "./stocks.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }
}
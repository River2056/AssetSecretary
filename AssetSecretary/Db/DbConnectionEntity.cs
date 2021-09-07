using Microsoft.EntityFrameworkCore;

namespace AssetSecretary.Db
{
    public class DbConnectionEntity : DbContext
    {
        public DbSet<StockObject> daily_price { get; set; }
        public string DbPath { get; set; }

        public DbConnectionEntity()
        {
            DbPath = "C:\\Users\\user\\Desktop\\stocks.db";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }
    }

    public class StockObject
    {
        public int Id { get; set; }
        public string Stock_Id { get; set; }
        public string Stock_Name { get; set; }
        public double Current_Price { get; set; }
        
        public override string ToString()
        {
            return $"id: {Id}, stock_id: {Stock_Id}, stock_name: {Stock_Name}, current_price: {Current_Price}";
        }
    }
}
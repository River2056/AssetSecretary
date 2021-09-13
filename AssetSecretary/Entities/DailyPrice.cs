using System.ComponentModel.DataAnnotations.Schema;

namespace AssetSecretary.Entities
{
    [Table(name: "DAILY_PRICE")]
    public class DailyPrice
    {
        public int Id { get; set; }
        [Column(name: "stock_id")]
        public string StockId { get; set; }
        [Column(name: "stock_name")]
        public string StockName { get; set; }
        [Column(name: "current_price")]
        public double CurrentPrice { get; set; }
        
        public override string ToString()
        {
            return $"id: {Id}, stock_id: {StockId}, stock_name: {StockName}, current_price: {CurrentPrice}";
        }
    }
}
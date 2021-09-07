using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AssetSecretary.Db;

namespace AssetSecretary
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DbConnectionEntity db = new DbConnectionEntity())
            {
                List<StockObject> list = db.daily_price.ToList();
                foreach (StockObject s in list)
                {
                    Console.WriteLine(s.ToString());
                }
            }
        }
    }
}
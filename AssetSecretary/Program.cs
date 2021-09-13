using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AssetSecretary.Db;
using AssetSecretary.Entities;

namespace AssetSecretary
{
    class Program
    {
        static void Main(string[] args)
        {
            using (DbConnectionEntity db = new DbConnectionEntity())
            {
                List<DailyPrice> list = db.DailyPrice.ToList();
                foreach (DailyPrice s in list)
                {
                    Console.WriteLine(s.ToString());
                }
            }
        }
    }
}
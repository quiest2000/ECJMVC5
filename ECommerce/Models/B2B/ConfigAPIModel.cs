using System;
using System.Data.Entity;
using System.Linq;
using ECommerce.Models.Domain;
using ECommerce.Models.Domain.EfModels;

namespace ECommerce.Models.B2B
{
    public class ConfigAPIModel
    {
        public bool ThemmoiConfig(ConfigAPI a)
        {
            using (var db = new MainContext())
            {
                try
                {
                    if (kiemtratontai(a.NhaCungCapId, db))
                    {
                        db.Entry(a).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        db.ConfigApIs.Add(a);
                        db.SaveChanges();
                    }
                    return true;
                }
                catch (Exception e) { return false; }
            }
        }
        private bool kiemtratontai(int nccId, MainContext db)
        {
            var config = (from p in db.ConfigApIs where p.NhaCungCapId == nccId select new { MaNCC = p.NhaCungCapId }).ToList();
            if (config.Count == 0)
                return false;
            return true;
        }
        public ConfigAPI getConfig(int nccId)
        {
            using (var db = new MainContext())
            {
                var config = (from p in db.ConfigApIs where p.NhaCungCapId == nccId select p).FirstOrDefault();
                return config;
            }
        }
    }
}
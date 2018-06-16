using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EC_TH2012_J.Models.Domain;
using EC_TH2012_J.Models.Domain.EfModels;

namespace EC_TH2012_J.Models
{
    public class ConfigAPIModel
    {
        public bool ThemmoiConfig(ConfigAPI a)
        {
            using(MainContext db = new MainContext())
            {
                try
                {
                    if (kiemtratontai(a.MaNCC, db))
                    {
                        db.Entry(a).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                    else
                    {
                        db.ConfigAPIs.Add(a);
                        db.SaveChanges();
                    }
                    return true;
                }
                catch (Exception e) { return false; }
            }
        }
        private bool kiemtratontai(string NCC,MainContext db)
        {
            var config = (from p in db.ConfigAPIs where p.MaNCC == NCC select new { p.MaNCC }).ToList();
            if (config.Count == 0)
                return false;
            return true;
        }
        public ConfigAPI getConfig(string MaNCC)
        {
            using(MainContext db = new MainContext())
            {
                var config = (from p in db.ConfigAPIs where p.MaNCC == MaNCC select p).FirstOrDefault();
                return config;
            }
        }
    }
}
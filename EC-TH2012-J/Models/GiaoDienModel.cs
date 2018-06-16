using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EC_TH2012_J.Models.Domain;
using EC_TH2012_J.Models.Domain.EfModels;

namespace EC_TH2012_J.Models
{
    public class GiaoDienModel
    {
        private MainContext db = new MainContext();

        internal IQueryable<GiaoDien> GetDD()
        {
            return db.GiaoDiens;
        }

        internal IQueryable<Link> GetSlideShow()
        {
            return db.Links.Where(m => m.Group.Contains("SlideShow"));
        }
    }
}
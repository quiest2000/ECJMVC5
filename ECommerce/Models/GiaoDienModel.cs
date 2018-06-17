using System.Linq;
using ECommerce.Models.Domain;
using ECommerce.Models.Domain.EfModels;

namespace ECommerce.Models
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
using System.Collections.Generic;
using System.Linq;
using ECommerce.Models.Domain;
using ECommerce.Models.Domain.EfModels;

namespace ECommerce.Models.B2B
{
    public class DoiTacModel
    {
        public List<NhaCungCap> LayDoitac()
        {
            using(var db = new MainContext())
            {
                var ds = (from p in db.NhaCungCaps where p.NetUserId == null select p).ToList();
                //var ds = (from p in db.NhaCungCaps select p).ToList();
                return ds;
            }
        }
    }
}
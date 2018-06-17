using System.Collections.Generic;
using System.Linq;
using EC_TH2012_J.Models.Domain;
using EC_TH2012_J.Models.Domain.EfModels;

namespace EC_TH2012_J.Models.B2B
{
    public class DoiTacModel
    {
        public List<NhaCungCap> LayDoitac()
        {
            using(var db = new MainContext())
            {
                var ds = (from p in db.NhaCungCaps where p.Net_user == null select p).ToList();
                //var ds = (from p in db.NhaCungCaps select p).ToList();
                return ds;
            }
        }
    }
}
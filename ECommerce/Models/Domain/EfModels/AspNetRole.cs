using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerce.Models.Domain.EfModels
{
    public class AspNetRole
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }

        private ICollection<AspNetUser> _aspNetUsers;
        public virtual ICollection<AspNetUser> AspNetUsers
        {
            get => _aspNetUsers ?? (_aspNetUsers = new List<AspNetUser>());
            set => _aspNetUsers = value;
        }
    }
}

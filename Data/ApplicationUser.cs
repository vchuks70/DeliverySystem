using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Data
{
  public  class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
            public ICollection<Route> Routes { get; set; }
        public bool IsAvaliable { get; set; } = true;
        public bool IsCourier { get; set; } = false;

        public ApplicationUser()
        {
            Routes = new Collection<Route>();
        }
    }
}

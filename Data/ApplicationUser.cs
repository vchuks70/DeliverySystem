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

        public ApplicationUser()
        {
            Routes = new Collection<Route>();
        }
    }
}

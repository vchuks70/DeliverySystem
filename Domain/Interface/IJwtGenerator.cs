using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;

namespace Domain.Interface
{
    public interface IJwtGenerator
    {
        string CreateToken(ApplicationUser user, string roles);
    }
}

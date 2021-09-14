using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helper.Response
{
 public   class LoginResponse
    {
            public bool Status { get; set; }
            public string Message { get; set; }
        public string Token { get; set; }

    }
}

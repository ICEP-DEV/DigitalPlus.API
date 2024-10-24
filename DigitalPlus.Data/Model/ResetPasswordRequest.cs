using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalPlus.Data.Model
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }  // This will be used to find the user
        public string NewPassword { get; set; }  // The new password to set
    }

}

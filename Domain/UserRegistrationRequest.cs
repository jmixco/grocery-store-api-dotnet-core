using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreAPI.Domain
{
    public class UserRegistrationRequest
    {
        [EmailAddress]
        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string Password { get; set; }
    }

    public class UserRegistrationResult
    {
        public string Token { get; set; }

        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}

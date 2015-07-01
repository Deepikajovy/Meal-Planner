using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Backend.Models
{
    public class OauthComeBack
    {
        public String access_token { get; set; }
        public String token_type { get; set; }
        public String expires_in { get; set; }
        public String state { get; set; }
    }
}
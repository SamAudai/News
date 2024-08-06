using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace News.Shared.DTOs.Account
{
    public class LoginResponsDto
    {      
        public bool isLogin { get; set; }        
        public IEnumerable<string> Errors { get; set; }
        public string Token { get; set; }
    }
}

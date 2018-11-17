using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Encoding.Models
{
    public class User
    {
        [Key]
        public Guid Token { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> Queries { get; set; }
        public User()
        {
            Queries = new List<string>();
        }
    }
}
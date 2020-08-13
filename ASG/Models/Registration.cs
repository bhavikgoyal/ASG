using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ASG.Models
{
    public class Registration
    {
        public string Name { get; set; }
       public String Email { get; set; }
       public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        [DisplayName("I Read and Accept")]
        public bool IReadAndAccept { get; set; }
    }
}
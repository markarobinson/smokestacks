using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace smokeStacks.Models
{
    public class ContactModel
    {
        public ContactModel()
        {
        }
    
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        public string Message { get; set; }

    }

}
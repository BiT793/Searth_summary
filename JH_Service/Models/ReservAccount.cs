using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JH_Service.Models
{
    public class ReservAccount
    {
        [Required] public Guid Id { get; set; }
        [Required] public int access_level { get; set; }
        [Required] public string password { get; set; }
        [Required] public string login { get; set; }
    }
}
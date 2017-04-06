using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallchat.Model.App.Entity
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string RoleName { get; set; }
    }
}

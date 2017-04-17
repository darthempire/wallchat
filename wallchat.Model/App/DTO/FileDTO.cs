using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wallchat.Model.App.DTO
{
    public class FileDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsDelete { get; set; }
        public double Size { get; set; }
        public DateTime DeleteDate { get; set; }
        public string Description { get; set; }
        public long UserId { get; set; }
    }

    public class RegisterFileDTO
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public double Size { get; set; }
    }
}

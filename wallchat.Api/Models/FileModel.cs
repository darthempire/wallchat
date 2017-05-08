using System;

namespace wallchat.Api.Models
{
    public class FileModel
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
}
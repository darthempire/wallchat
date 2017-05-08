﻿using System;

namespace wallchat.Model.App.Entity
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsDelete { get; set; }
        public double Size { get; set; }
        public string Description { get; set; }

        public long UserId { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public abstract class Post
    {
        public User Submitter { get; set; }
        public string Content { get; set; }
        public DateTime PostDT { get; set; }
        public int Likes { get; set; }
    }
}

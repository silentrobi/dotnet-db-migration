using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostApi.Models
{
    public class Post : Common
    {
        [Key]
        public Guid Id { get; set; }
        public string Content { get; set; }

        public IList<PostCategory> PostCategories { get; set; }
    }
}

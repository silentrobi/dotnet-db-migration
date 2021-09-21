using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PostApi.Models
{
    public class Category : Common
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IList<PostCategory> PostCategories { get; set; }
    }
}

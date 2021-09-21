using System;
using System.ComponentModel.DataAnnotations;

namespace PostApi.Models
{
    public class PostCategory : Common
    {
        public Guid PostId { get; set; }
        public Post Post { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
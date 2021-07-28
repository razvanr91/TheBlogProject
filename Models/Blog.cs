using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheBlogProject.Models
{
    public class Blog
    {
        public int Id { get; set; }

        [Display(Name= "Author")]
        public string AuthorId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage ="The {0} must be between {2} and {1} characters long.",MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be between {2} and {1} characters long.", MinimumLength = 2)]
        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created on ")]
        public DateTime Created { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Updated on ")]
        public DateTime? Updated { get; set; }

        [Display(Name = "Blog Image")]
        public byte[] ImageData { get; set; }

        [Display(Name = "Image Type")]
        public string ContentType { get; set; }

        [NotMapped]
        public IFormFile Image { get; set; }

        // Navigation Properties

        public virtual BlogUser Author { get; set; }

        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();
    }
}

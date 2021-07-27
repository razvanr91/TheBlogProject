using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TheBlogProject.Models
{
    public class BlogUser : IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters.", MinimumLength = 1)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "The {0} must be between {2} and {1} characters.", MinimumLength = 1)]
        public string LastName { get; set; }

        [Display(Name = "Image")]
        public byte[] ImageData { get; set; }

        [Display(Name = "Image Type")]
        public string ContentType { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be between {2} and {1} characters.", MinimumLength = 1)]
        public string LinkedInUrl { get; set; }

        [StringLength(100, ErrorMessage = "The {0} must be between {2} and {1} characters.", MinimumLength = 1)]
        public string GitHubUrl { get; set; }

        [NotMapped]
        public string FullName { get { return $"{FullName} {LastName}"; } }

        // Navigation Properties

        public virtual ICollection<Blog> Blogs { get; set; } = new HashSet<Blog>();

        public virtual ICollection<Post> Posts { get; set; } = new HashSet<Post>();

        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();

    }
}

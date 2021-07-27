using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TheBlogProject.Enums;

namespace TheBlogProject.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string AuthorId { get; set; }

        public string ModeratorId { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "The {0} must be between {2} and {1} characters.", MinimumLength = 2)]
        [Display(Name = "Comment")]
        public string Body { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Created on")]
        public DateTime Created { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Updated on")]
        public DateTime? Updated { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Moderated on")]
        public DateTime? Moderated { get; set; }

        [DataType(DataType.DateTime)]
        [Display(Name = "Removed on")]
        public DateTime? Deleted { get; set; }

        [StringLength(500, ErrorMessage = "The {0} must be between {2} and {1} characters.", MinimumLength = 2)]
        [Display(Name = "Moderated Comment")]
        public string ModeratedBody { get; set; }

        public ModerationType ModerationType { get; set; }

        // Navigation Properties

        public virtual Post Post { get; set; }

        public virtual BlogUser Author { get; set; }

        public virtual BlogUser Moderator { get; set; }
    }
}

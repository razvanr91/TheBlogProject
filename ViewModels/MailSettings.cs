using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBlogProject.ViewModels
{
    public class MailSettings
    {
        // Configure and use SMTP Server
        // from Google
        public string Mail { get; set; }

        [Display(Name = "Display Name")]
        public string DisplayName { get; set; }

        public string Password { get; set; }

        public string Host { get; set; }

        public int Port { get; set; }

    }
}

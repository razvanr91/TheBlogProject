using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheBlogProject.Enums
{
    public enum ReadyStatus
    {
        Incomplete,

        [Display(Name = "Production Ready")]
        ProductionReady,

        [Display(Name = "Preview Ready")]
        PreviewReady
    }
}

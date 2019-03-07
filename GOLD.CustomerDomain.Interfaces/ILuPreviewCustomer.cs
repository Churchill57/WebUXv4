using GOLD.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.CustomerDomain.Interfaces
{
    public interface ILuPreviewCustomer
    {

        EntityContext CustomerContext { get; set; }

        bool PreviewDifferentCustomer { get; set; }

        bool ShowBackButton { get; set; }

        string BackButtonText { get; set; } 

        bool BackButtonAsLink { get; set; }

        string DoneButtonText { get; set; }

    }
}

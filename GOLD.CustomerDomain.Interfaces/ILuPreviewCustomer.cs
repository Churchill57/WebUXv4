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
        EntityContext CustomerToPreview { get; set; }
    }
}

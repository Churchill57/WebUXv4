using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.AppManagement.Interfaces
{
    public interface IExecutionManager
    {
        Task<string> RedirectLaunchAppAsync(string componentInterfaceFullname, string returnUrl);
        Task<string> RedirectResumeExecutionThreadAsync(int ID);
    }
}

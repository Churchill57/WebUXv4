using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.AppManagement.Interfaces
{
    public interface IExecutionManager
    {
        Task<string> RedirectLaunchApp(string componentInterfaceFullname);
        //string StartExecutionThread(Type componentInterface);
    }
}

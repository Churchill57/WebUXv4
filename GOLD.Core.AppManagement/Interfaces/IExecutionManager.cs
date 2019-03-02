using GOLD.AppExecution.ApiModels;
using GOLD.Core.Components;
using GOLD.Core.Models;
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
        //Task<T> LoadLogicalUnitFromExecutionThreadAsync<T>(TXID txid) where T : LogicalUnit, new();
        Task<T> LoadComponentFromExecutionThreadAsync<T>(TXID txid) where T : Component, new();
        Task<T> LoadComponentFromExecutionThreadAsync<T>(string txid) where T : Component, new();
        //T LoadComponentFromExecutionThread<T>(ExecutionThread executionThread, int xid) where T : Component, new();
        Task SaveComponentToExecutionThreadAsync(Component component);

    }
}

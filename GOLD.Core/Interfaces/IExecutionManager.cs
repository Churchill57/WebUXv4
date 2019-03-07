using GOLD.AppExecution.ApiModels;
using GOLD.Core.Components;
using GOLD.Core.Models;
using GOLD.Core.Outcomes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.Interfaces
{
    public interface IExecutionManager
    {
        Task<string> RedirectLaunchAppAsync(string componentInterfaceFullname, string returnUrl);
        Task<string> ExecuteLogicalUnitAsync<T>(string txid) where T : LogicalUnit, new();
        //Task<string> RedirectResumeExecutionThreadAsync(int ID);
        //Task<T> LoadComponentFromExecutionThreadAsync<T>(TXID txid) where T : Component, new();
        Task<T> LoadComponentFromExecutionThreadAsync<T>(string txid) where T : Component, new();
        //Task SaveComponentToExecutionThreadAsync(Component component);
        Task<T> LoadComponentInterfaceFromExecutionThreadAsync<T>(string txid) where T : class;
        Task<IComponent> LoadComponentAsync(string txid);
        Task<string> RaiseOutcomeAsync(IComponent sourceComponent, Outcome outcome);
        Task<string> RaiseOutcomeAsync(ITXID sourceTXID, Outcome outcome);
        //T NewOutcome<T>() where T : class;

    }
}

using GOLD.AppExecution.ApiModels;
using GOLD.Core.Components;
using GOLD.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.Interfaces
{
    internal interface IExecutionManagerInternal
    {
        Task<string> RedirectLaunchAppAsync(string componentInterfaceFullname, string returnUrl);
        Task<string> RedirectLaunchAppAsync(ITXID parentTXID, string componentInterfaceFullname);
        Task<string> RedirectResumeExecutionThreadAsync(int ID);
        //Task<T> LoadComponentFromExecutionThreadAsync<T>(TXID txid) where T : Component, new();
        Task<T> LoadComponentFromExecutionThreadAsync<T>(string txid) where T : Component, new();
        T LoadComponentFromExecutionThread<T>(string txid) where T : Component, new();
        Task SaveComponentToExecutionThreadAsync(Component component);
        //T ExtractComponentFromExecutionThread<T>(Component parentComponent, string clientRef) where T : Component, new();
        void SaveComponentToExecutionThread(Component component);
        Task<T> GetComponentAsync<T>(Component parentComponent, string clientRef) where T : Component, new();
        Task<IComponent> GetComponentInterfaceAsync<T>(Component parentComponent, string clientRef) where T : class;//, IComponent;
        Task<T> LoadComponentInterfaceFromExecutionThreadAsync<T>(string txid) where T : class;
    }
}

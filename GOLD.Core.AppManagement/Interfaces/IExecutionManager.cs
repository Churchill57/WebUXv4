using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.AppManagement.Interfaces
{
    public interface IExecutionManager
    {
        string StartExecutionThread(Type componentInterface);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.Interfaces
{
    public interface IUxPerformSearch<T>
    {
        Task<IEnumerable<T>> PerformSearchAsync();
    }
}

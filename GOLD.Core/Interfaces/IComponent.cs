﻿using GOLD.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.Interfaces
{
    public interface IComponent
    {
        TXID TXID { get; set; }
    }
}

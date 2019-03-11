using GOLD.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GOLD.Core.Models
{
    public class TXID : ITXID
    {
        private int _tid;
        private int _xid;

        private TXID()
        {

        }

        public TXID(int tid, int xid)
        {
            _tid = tid;
            _xid = xid;
        }

        public TXID(string txid)
        {
            var parts = txid.Split('-');
            int.TryParse(parts[0], out _tid);
            int.TryParse(parts[1], out _xid);
        }
        public int tid { get => _tid; }

        public int xid { get => _xid; }

        public override string ToString()
        {
            return $"{_tid}-{_xid}";
        }
    }
}

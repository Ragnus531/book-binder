using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Utils
{
    public class StateContainer
    {
        public readonly Dictionary<int, object> ObjectTunnel = new();
    }
}

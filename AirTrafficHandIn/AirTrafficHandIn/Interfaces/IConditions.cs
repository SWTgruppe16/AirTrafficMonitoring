using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficHandIn
{
    public interface IConditions
    {
        List<string> ListOfConditions(List<Tracks> tracks);
    }
}

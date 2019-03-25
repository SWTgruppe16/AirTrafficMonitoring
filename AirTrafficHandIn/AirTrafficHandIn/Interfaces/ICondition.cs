using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirTrafficHandIn
{
    public interface ICondition
    {
        DateTime TimeOfOccurance { get; }
        List<string> InvolvedTagIds { get; }
        String TypeOfCondition { get; }
    }
}

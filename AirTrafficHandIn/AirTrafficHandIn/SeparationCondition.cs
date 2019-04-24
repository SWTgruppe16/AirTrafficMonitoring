using System;
using System.Collections.Generic;

namespace AirTrafficHandIn
{
    public class SeparationCondition : ICondition
    {
        private long v1;
        private string v2;
        private string v3;

        public DateTime TimeOfOccurance { get; set;  }
        public List<string> InvolvedTagIds{ get; set; }
        public string TypeOfCondition { get; private set; }

        public string ID
        {
            get
            {
                var a = InvolvedTagIds[0];
                var b = InvolvedTagIds[1];
                if (a.CompareTo(b) > 0)
                {
                    var tmp = a;
                    a = b;
                    b = tmp;
                }
                                                    
                return a + b;
            }
        }

        public SeparationCondition(DateTime datetime, string tag_a, string tag_b)
        {
            TypeOfCondition = "separation";
            InvolvedTagIds = new List<string> { tag_a, tag_b };
            TimeOfOccurance = datetime;
        }
    }
}
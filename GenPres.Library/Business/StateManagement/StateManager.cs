using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business
{
    
    public class StateManager
    {
        private List<string> propertyStates = new List<string>();

        public enum ValueState
        {
            NotSet,
            Dirty,
            Calculated,
            User
        }
    }
    
}

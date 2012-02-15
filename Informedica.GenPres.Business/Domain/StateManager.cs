using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.Domain
{
    
    public class StateManager
    {
        private List<string> propertyStates = new List<string>();

        public enum State
        {
            NotSet,
            Dirty,
            Calculated,
            User
        }
    }
    
}

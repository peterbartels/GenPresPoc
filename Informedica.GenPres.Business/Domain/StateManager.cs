using System.Collections.Generic;

namespace Informedica.GenPres.Business.Domain
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GenPres.Business;

namespace GenPres.Operations.Determination.Rules
{
    class TPNSolution : IDetermination
    {
        private Dictionary<string, Dictionary<string, decimal>> Solutions;

        public TPNSolution(Dictionary<string, Dictionary<string, decimal>> solutions)
        {
            Solutions = solutions;
        }
        public bool Determine(Prescription p)
        {   
            ComponentCollection components = p.Drug.Components;
            if (components.Count < 1) return false;

            Component solutionComponent = (from i in components where i.IsSolution == true select i).SingleOrDefault();

            if (solutionComponent == null) return false;

            decimal val = 0;
            //decimal qty = 0;
            for (int c = 0; c < components.Count; c++)
            {
                Component currentComponent = components[c];
                if(Solutions.Keys.Contains<string>(currentComponent.Name))
                {
                    for (int i = 0; i < Solutions[currentComponent.Name].Count; i++)
                    {
                        string key = Solutions[currentComponent.Name].Keys.ElementAt<string>(i);
                        if (solutionComponent.Name == key)
                        {
                            val = Solutions[currentComponent.Name][key];
                            currentComponent.SolutionRelation = val;
                        }
                    }    
                }
            }
            return true;
        }
    }
}

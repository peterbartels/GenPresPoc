
using System.Collections.Generic;

namespace GenPres.Business.Domain.Prescriptions
{
    public interface IDrug : ISavable
    {
        string Generic { get; set; }
        string Route { get; set; }
        string Shape { get; set; }
        List<IComponent> Components { get; set; }
        void CheckIncrements();
    }
}

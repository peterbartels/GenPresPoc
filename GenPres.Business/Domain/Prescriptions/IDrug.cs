
using System.Collections.Generic;
using GenPres.Business.Domain.Units;

namespace GenPres.Business.Domain.Prescriptions
{
    public interface IDrug : ISavable
    {
        string Generic { get; set; }
        string Route { get; set; }
        string Shape { get; set; }
        List<IComponent> Components { get; set; }
        UnitValue Quantity { get; set; }
        void CheckIncrements();
    }
}

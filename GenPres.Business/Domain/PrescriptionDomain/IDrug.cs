
namespace GenPres.Business.Domain.PrescriptionDomain
{
    public interface IDrug : ISavable
    {
        string Generic { get; set; }
        string Route { get; set; }
        string Shape { get; set; }
    }
}

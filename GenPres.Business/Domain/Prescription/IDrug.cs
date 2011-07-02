
namespace GenPres.Business.Domain.Prescription
{
    public interface IDrug
    {
        string Generic { get; set; }
        string Route { get; set; }
        string Shape { get; set; }
    }
}

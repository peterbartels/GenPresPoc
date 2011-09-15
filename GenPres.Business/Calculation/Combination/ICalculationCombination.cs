
namespace GenPres.Business.Calculation.Combination
{
    public interface ICalculationCombination
    {
        void Calculate();
        void Finish();
        bool CanBeCalculated();
        void ConvertCombinationsValuesToArray();
        void CorrectPropertyIncrements();
        int GetUserCount();
    }
}

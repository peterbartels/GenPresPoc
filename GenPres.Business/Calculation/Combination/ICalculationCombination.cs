
namespace GenPres.Business.Calculation.Combination
{
    public interface ICalculationCombination
    {
        void Calculate();
        void Finish();
        void ConvertCombinationsValuesToArray();
        void CorrectPropertyIncrements();
    }
}

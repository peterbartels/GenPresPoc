namespace Informedica.GenPres.Business.Allowance
{
    public class PropertyAllowanceConfig
    {
        private bool _allow;
        private IPropertyAllowance _property;

        public PropertyAllowanceConfig(bool allow, IPropertyAllowance property)
        {
            _allow = allow;
            _property = property;
        }

        public void SetPropertyAllowance()
        {
            _property.CanBeSet = _allow;
        }
    }
}

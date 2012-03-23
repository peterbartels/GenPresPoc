namespace Informedica.GenPres.Data.Visibility
{
    public class PropertyVisibilityConfig
    {
        private bool _allow;
        private IPropertyVisibility _property;

        public PropertyVisibilityConfig(bool allow, IPropertyVisibility property)
        {
            _allow = allow;
            _property = property;
        }

        public void SetPropertyAllowance()
        {
            _property.IsVisible = _allow;
        }
    }
}

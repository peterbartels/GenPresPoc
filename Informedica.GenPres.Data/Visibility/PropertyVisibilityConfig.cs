namespace Informedica.GenPres.Data.Visibility
{
    public static class PropertyVisibilityConfig
    {
        public static void SetPropertyAllowance(IVisibility property, bool allow)
        {
            property.visible = allow;
        }
    }
}

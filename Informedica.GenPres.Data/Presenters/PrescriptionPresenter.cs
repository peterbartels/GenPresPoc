using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Informedica.GenPres.Data.Presenters
{
    public class PrescriptionPresenter
    {
        private static IPrescriptionVisibilityManager _prescriptionVisibilityManager = new PrescriptionVisibilityManager();
        
        public static IPrescriptionVisibilityManager GetVisibilityManager()
        {
            return _prescriptionVisibilityManager;
        }

        public void SetVisibility()
        {
            GetVisibilityManager().ApplyToDto();
        }
    }

    public class PrescriptionVisibilityManager : IPrescriptionVisibilityManager
    {
        public bool ApplyToDto()
        {
            return false;
        }
    }

    public interface IPrescriptionVisibilityManager
    {
        bool ApplyToDto();
    }
}

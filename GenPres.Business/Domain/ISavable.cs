using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.Domain
{
    public interface ISavable
    {
        bool IsNew { get; }
        void OnCreate();
        void OnNew();
        void OnInitExisting();
        int Id { get; set; }
    }
}

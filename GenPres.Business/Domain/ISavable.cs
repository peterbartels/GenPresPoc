using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.Domain
{
    public interface ISavable
    {
        bool IsNew { get; set; }
        void OnCreate();
        void OnNew();
        void OnInitExisting();
        void Save();
        int Id { get; set; }
    }
}

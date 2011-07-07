using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenPres.Business.Domain
{
    public interface IUser
    {
        int Id { get; set; }
        string UserName { get; set; }
        string PassCrypt { get; set; }
    }
}

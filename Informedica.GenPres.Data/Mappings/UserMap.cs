using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using GenPres.Business.Domain.Users;

namespace GenPres.Data.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(s => s.Id).GeneratedBy.GuidComb();
            Map(s => s.PassCrypt);
            Map(s => s.UserName);
        }
    }
}

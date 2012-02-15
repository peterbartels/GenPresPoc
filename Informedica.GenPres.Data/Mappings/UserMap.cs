using FluentNHibernate.Mapping;
using Informedica.GenPres.Business.Domain.Users;

namespace Informedica.GenPres.Data.Mappings
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

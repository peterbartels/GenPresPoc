using System;
using Informedica.DataAccess.Mappings;
using Informedica.GenPres.xTest.Data.SessionFactoryTest.Fixtures.Class;

namespace Informedica.GenPres.xTest.Data.SessionFactoryTest.Fixtures.Map
{
    class EntityClassMap : EntityMap<EntityClass, Guid>
    {
        public EntityClassMap()
        {
            Map(x => x.Name);
        }
    }
}

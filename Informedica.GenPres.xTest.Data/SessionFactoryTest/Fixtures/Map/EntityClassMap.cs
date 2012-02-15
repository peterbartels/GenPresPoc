using System;
using GenPres.xTest.Data.SessionFactoryTest.Fixtures.Class;
using Informedica.DataAccess.Mappings;

namespace GenPres.xTest.Data.SessionFactoryTest.Fixtures.Map
{
    class EntityClassMap : EntityMap<EntityClass, Guid>
    {
        public EntityClassMap()
        {
            Map(x => x.Name);
        }
    }
}

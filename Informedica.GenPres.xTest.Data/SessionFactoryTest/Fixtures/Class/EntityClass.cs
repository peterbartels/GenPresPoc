using System;
using Informedica.EntityRepository.Entities;

namespace Informedica.GenPres.xTest.Data.SessionFactoryTest.Fixtures.Class
{
    public class EntityClass : Entity<EntityClass, Guid>
    {
        public virtual string Name { get; set; }
        public virtual int ProductNr { get; set; }

        public override bool IsIdentical(EntityClass entity)
        {
            return false;
        }
    }
}

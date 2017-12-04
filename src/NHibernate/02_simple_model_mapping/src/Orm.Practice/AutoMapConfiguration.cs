using System;
using FluentNHibernate.Automapping;

namespace Orm.Practice
{
    class AutoMapConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            return type == typeof(Address);
        }
    }
}
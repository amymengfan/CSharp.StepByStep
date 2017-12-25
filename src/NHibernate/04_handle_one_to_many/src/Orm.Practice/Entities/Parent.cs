using System;
using System.Collections.Generic;
using FluentNHibernate.Mapping;

namespace Orm.Practice.Entities
{
    public class Parent
    {
        public virtual Guid ParentId { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Child> Children { get; set; }
        public virtual bool IsForQuery { get; set; }
    }

    public class ParentMap : ClassMap<Parent>
    {
        public ParentMap()
        {
            #region Please modify the code to pass the test

            Table("parent");
            Id(e => e.ParentId).Column("ParentId").GeneratedBy.Guid();
            Map(e => e.Name).Column("Name");
            Map(e => e.IsForQuery).Column("IsForQuery");
            HasMany(e => e.Children).KeyColumn("ParentID").Inverse().Cascade.AllDeleteOrphan();

            #endregion
        }
    }
}
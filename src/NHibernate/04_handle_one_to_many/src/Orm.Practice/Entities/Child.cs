using System;
using FluentNHibernate.Mapping;

namespace Orm.Practice.Entities
{
    public class Child
    {
        public virtual Guid ChildId { get; set; }
        public virtual string Name { get; set; }
        public virtual bool IsForQuery { get; set; }

        #region You can add some code here if you want

        // It is totally okay if you do not want to do anything here.
        public virtual Parent Parent { get; set; }

        #endregion
    }

    public class ChildMap : ClassMap<Child>
    {
        public ChildMap()
        {
            #region Please modify the code to pass the test

            Table("child");
            Id(e => e.ChildId).Column("ChildId").GeneratedBy.Guid();
            Map(e => e.Name).Column("Name");
            Map(e => e.IsForQuery).Column("IsForQuery");
            References(e => e.Parent).Column("ParentID");

            #endregion
        }
    }
}
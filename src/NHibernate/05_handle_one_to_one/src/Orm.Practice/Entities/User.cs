using System;
using FluentNHibernate.Mapping;

namespace Orm.Practice.Entities
{
    public class User
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual bool IsForQuery { get; set; }
        public virtual Preference Preference { get; set; }
    }

    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            #region Please modify the code to pass the test

            Table("[dbo].[User]");
            Id(e => e.Id).Column("Id").GeneratedBy.Guid();
            Map(e => e.Name).Column("Name");
            Map(e => e.IsForQuery).Column("IsForQuery");
            HasOne(e => e.Preference).Constrained().Cascade.All().ForeignKey("none");

            #endregion
        }
    }
}
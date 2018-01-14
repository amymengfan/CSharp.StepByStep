using System;
using FluentNHibernate.Mapping;

namespace Orm.Practice.Entities
{
    public class Preference
    {
        public virtual Guid UserId { get; set; }
        public virtual string Theme { get; set; }
        public virtual bool IsForQuery { get; set; }
        public virtual User User { get; set; }
    }

    public class PreferenceMap : ClassMap<Preference>
    {
        public PreferenceMap()
        {
            #region Please modify the code to pass the test

            Table("[dbo].[Preference]");
            Id(e => e.UserId).Column("UserId").GeneratedBy.Foreign("User");
            Map(e => e.Theme).Column("Theme");
            Map(e => e.IsForQuery).Column("IsForQuery");
            HasOne(e => e.User).Constrained().Cascade.All();

            #endregion
        }
    }
}
using System.Linq;
using NHibernate.Linq;
using Orm.Practice.Entities;
using Xunit;
using Xunit.Abstractions;

namespace Orm.Practice
{
    public class OneToOneFacts : OrmFactBase
    {
        public OneToOneFacts(ITestOutputHelper output) : base(output)
        {
            ExecuteSQL("DELETE FROM [dbo].[User] WHERE IsForQuery=0");
            ExecuteSQL("DELETE FROM [dbo].[Preference] WHERE IsForQuery=0");
        }

        [Fact]
        void should_load_all_user()
        {
            var users = Session.Query<User>()
                .Where(e => e.IsForQuery)
                .Fetch(e => e.Preference)
                .OrderBy(e => e.Name)
                .ToList();

            Assert.Equal(new[] {"user-query-1", "user-query-2"}, users.Select(e => e.Name));
            Assert.Equal(new[] {"Dark", "Monokai"}, users.Select(e => e.Preference.Theme));
        }

        [Fact]
        void should_load_all_preference()
        {
            var preferences = Session.Query<Preference>()
                .Where(e => e.IsForQuery)
                .Fetch(e => e.User)
                .OrderBy(e => e.Theme)
                .ToList();

            Assert.Equal(new[] {"Dark", "Monokai"}, preferences.Select(e => e.Theme));
            Assert.Equal(new[] {"user-query-1", "user-query-2"}, preferences.Select(e => e.User.Name));
        }

        [Fact]
        void should_insert_user_with_preference()
        {
            var user = new User
            {
                Name = "user-non-query-1",
                IsForQuery = false,
            };
            var preference = new Preference
            {
                Theme = "Light",
                IsForQuery = false,
                User = user
            };
            user.Preference = preference;

            Session.Save(user);
            Session.Flush();
            Session.Clear();

            var insertedUser = Session.Query<User>()
                .Fetch(e => e.Preference)
                .Single(e => !e.IsForQuery);

            Assert.Equal("user-non-query-1", insertedUser.Name);
            Assert.Equal("Light", insertedUser.Preference.Theme);
        }

        [Fact]
        void should_insert_preference_with_user()
        {
            var preference = new Preference
            {
                Theme = "Light",
                IsForQuery = false,
            };
            var user = new User
            {
                Name = "user-non-query-1",
                IsForQuery = false,
                Preference = preference
            };
            preference.User = user;

            Session.Save(preference);
            Session.Flush();
            Session.Clear();

            var insertedPreference = Session.Query<Preference>()
                .Fetch(e => e.User)
                .Single(e => !e.IsForQuery);

            Assert.Equal("Light", insertedPreference.Theme);
            Assert.Equal("user-non-query-1", insertedPreference.User.Name);
        }

        [Fact]
        void should_delete_in_a_cascade_manner()
        {
            var user = new User
            {
                Name = "user-non-query-1",
                IsForQuery = false,
            };
            var preference = new Preference
            {
                Theme = "Light",
                IsForQuery = false,
                User = user
            };
            user.Preference = preference;

            Session.Save(user);
            Session.Flush();
            Session.Clear();

            Session.Delete(Session.Query<User>().Single(e => e.Name == "user-non-query-1"));
            Session.Flush();
            Session.Clear();

            Assert.False(Session.Query<User>().Any(c => !c.IsForQuery));
            Assert.False(Session.Query<Preference>().Any(c => !c.IsForQuery));
        }
    }
}
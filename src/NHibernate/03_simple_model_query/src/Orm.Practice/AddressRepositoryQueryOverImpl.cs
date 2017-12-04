using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Criterion;

namespace Orm.Practice
{
    public class AddressRepositoryQueryOverImpl : RepositoryBase, IAddressRepository
    {
        public AddressRepositoryQueryOverImpl(ISession session) : base(session)
        {
        }

        public Address Get(int id)
        {
            #region Please implement the method

            return Session.QueryOver<Address>().Where(e => e.Id == id).SingleOrDefault();

            #endregion
        }

        public IList<Address> Get(IEnumerable<int> ids)
        {
            #region Please implement the method

            return Session.QueryOver<Address>()
                .WhereRestrictionOn(e => e.Id)
                .IsIn(ids.ToList())
                .OrderBy(e => e.Id).Asc
                .List();

            #endregion
        }

        public IList<Address> GetByCity(string city)
        {
            #region Please implement the method

            return Session.QueryOver<Address>()
                .Where(e => e.City == city)
                .OrderBy(e => e.Id).Asc
                .List();

            #endregion
        }

        public Task<IList<Address>> GetByCityAsync(string city)
        {
            #region Please implement the method

            return GetByCityAsync(city, CancellationToken.None);

            #endregion
        }

        public Task<IList<Address>> GetByCityAsync(string city, CancellationToken cancellationToken)
        {
            #region Please implement the method

            return Session.QueryOver<Address>()
                .Where(e => e.City == city)
                .OrderBy(e => e.Id).Asc
                .ListAsync(cancellationToken);

            #endregion
        }

        public IList<KeyValuePair<int, string>> GetOnlyTheIdAndTheAddressLineByCity(string city)
        {
            #region Please implement the method

            return Session.QueryOver<Address>()
                .Where(e => e.City == city)
                .OrderBy(e => e.Id).Asc
                .SelectList(e => e.Select(a => a.Id).Select(a => a.AddressLine1))
                .List<object[]>()
                .Select(e => new KeyValuePair<int, string>((int) e[0], (string) e[1]))
                .ToList();

            #endregion
        }

        public IList<string> GetPostalCodesByCity(string city)
        {
            #region Please implement the method

            return Session.QueryOver<Address>()
                .Where(e => e.City == city)
                .Select(Projections.Distinct(Projections.Property<Address>(e => e.PostalCode)))
                .List<string>();

            #endregion
        }
    }
}
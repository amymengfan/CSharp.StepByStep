using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;

namespace Orm.Practice
{
    public class AddressRepositoryLinqImpl : RepositoryBase, IAddressRepository
    {
        public AddressRepositoryLinqImpl(ISession session)
            : base(session)
        {
        }

        public Address Get(int id)
        {
            #region Please implement the method

            return Session.Query<Address>().SingleOrDefault(e => e.Id == id);

            #endregion
        }

        public IList<Address> Get(IEnumerable<int> ids)
        {
            #region Please implement the method

            return Session.Query<Address>()
                .Where(e => ids.Contains(e.Id))
                .OrderBy(e => e.Id)
                .ToList();

            #endregion
        }

        public IList<Address> GetByCity(string city)
        {
            #region Please implement the method

            return Session.Query<Address>()
                .Where(e => e.City == city)
                .OrderBy(e => e.Id)
                .ToList();

            #endregion
        }

        public Task<IList<Address>> GetByCityAsync(string city)
        {
            #region Please implement the method

            return GetByCityAsync(city, CancellationToken.None);

            #endregion
        }

        public async Task<IList<Address>> GetByCityAsync(
            string city, CancellationToken cancellationToken)
        {
            #region Please implement the method

            return await Session.Query<Address>()
                .Where(e => e.City == city)
                .OrderBy(e => e.Id)
                .ToListAsync(cancellationToken);

            #endregion
        }

        public IList<KeyValuePair<int, string>> GetOnlyTheIdAndTheAddressLineByCity(string city)
        {
            #region Please implement the method

            return Session.Query<Address>()
                .Where(e => e.City == city)
                .OrderBy(e => e.Id)
                .Select(e => new KeyValuePair<int, string>(e.Id, e.AddressLine1))
                .ToList();

            #endregion
        }

        public IList<string> GetPostalCodesByCity(string city)
        {
            #region Please implement the method

            return Session.Query<Address>()
                .Where(e => e.City == city)
                .Select(e => e.PostalCode)
                .Distinct()
                .ToList();

            #endregion
        }
    }
}
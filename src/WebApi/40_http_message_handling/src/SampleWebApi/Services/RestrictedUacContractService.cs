using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using SampleWebApi.DomainModel;
using SampleWebApi.Repositories;

namespace SampleWebApi.Services
{
    public class RestrictedUacContractService
    {
        readonly RoleRepository roleRepository;

        public RestrictedUacContractService(RoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public bool RemoveRestrictedInfo(long userId, JObject restrictedResource)
        {
            /*
             * The sensitive information should be removed if current user is not an
             * Admin.
             *
             * The sensitive information is placed in a property called 'links',
             * which is an array of objects. Each object should have an optional
             * boolean property called 'restricted'. And its value should be regarded
             * as `false` if the property is not defined.
             *
             * If the user is an Admin, then nothing should be changed, while if the
             * user is a normal user. Then all restricted inforamtion should be removed.
             *
             * The function will return true if the JSON content has been updated. Or
             * else it will return false.
             */
            if (roleRepository.Get(userId) == Role.Admin) return false;

            var links = restrictedResource["links"] as JArray;
            if (links == null) return false;

            var restrictedLinks = links.Where(e =>
            {
                var item = e as JObject;
                var value = item?["restricted"];
                return value?.Type == JTokenType.Boolean && value.Value<bool>();
            }).ToArray();
            if (restrictedLinks.Length == 0) return false;

            foreach (var link in restrictedLinks)
            {
                links.Remove(link);
            }
            return true;
        }
    }
}
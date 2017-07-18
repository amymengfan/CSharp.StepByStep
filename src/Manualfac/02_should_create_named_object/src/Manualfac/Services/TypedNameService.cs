using System;

namespace Manualfac.Services
{
    class TypedNameService : Service, IEquatable<TypedNameService>
    {
        #region Please modify the following code to pass the test

        /*
         * This class is used as a key for registration by both type and name.
         */
        readonly Type serviceType;
        readonly string name;

        public TypedNameService(Type serviceType, string name)
        {
            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));
            if (name == null) throw new ArgumentNullException(nameof(name));
            this.serviceType = serviceType;
            this.name = name;
        }

        public bool Equals(TypedNameService other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return serviceType == other.serviceType && name.Equals(other.name);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TypedNameService) obj);
        }

        public override int GetHashCode()
        {
            return serviceType.GetHashCode() ^ name.GetHashCode();
        }

        #endregion
    }
}
using System;

namespace Manualfac.Services
{
    class TypedService : Service, IEquatable<TypedService>
    {
        #region Please modify the following code to pass the test

        /*
         * This class is used as a key for registration by type.
         */
        readonly Type serviceType;

        public TypedService(Type serviceType)
        {
            if (serviceType == null) throw new ArgumentNullException(nameof(serviceType));
            this.serviceType = serviceType;
        }

        public bool Equals(TypedService other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return serviceType == other.serviceType;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((TypedService) obj);
        }

        public override int GetHashCode()
        {
            return serviceType.GetHashCode();
        }

        #endregion
    }
}
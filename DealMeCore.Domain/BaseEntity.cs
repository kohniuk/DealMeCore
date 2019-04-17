using System;

namespace DealMeCore.Domain
{
    /// <summary>
    /// Base Entity
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Gets unique identifier for base entity.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" /> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as BaseEntity);
        }

        /// <summary>
        /// Indicates whether the current <see cref="BaseEntity"/> is equal to another one of the same type.
        /// </summary>
        /// <param name="obj">An <see cref="BaseEntity"/> to compare with this one.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="BaseEntity" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool Equals(BaseEntity obj)
        {
            if (obj == null)
            {
                return false;
            }

            /* due to lazy load usage obj or this types can be a subclass of real entity type, 
             * so obj and this can be equal, but have different types. */
            if (!this.GetType().IsInstanceOfType(obj) &&
                !obj.GetType().IsInstanceOfType(this))
            {
                return false;
            }

            if (Id == default(Guid) && obj.Id == default(Guid))
            {
                return ReferenceEquals(this, obj);
            }

            return this.Id == obj.Id;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #region operators

        /// <summary>
        /// Returns a value indicating whether two <see cref="BaseEntity"/>s are equal.
        /// </summary>
        /// <param name="x">The first <see cref="BaseEntity"/> to compare.</param>
        /// <param name="y">The second <see cref="BaseEntity"/> to compare.</param>
        /// <returns>True if <paramref name="x"/> and <paramref name="y"/> are equal; otherwise, false.</returns>
        public static bool operator ==(BaseEntity x, BaseEntity y)
        {
            return object.Equals(x, y);
        }

        /// <summary>
        /// Returns a value indicating whether two <see cref="BaseEntity"/>s are not equal.
        /// </summary>
        /// <param name="x">The first <see cref="BaseEntity"/> to compare.</param>
        /// <param name="y">The second <see cref="BaseEntity"/> to compare.</param>
        /// <returns>True if <paramref name="x"/> and <paramref name="y"/> are not equal; otherwise, false.</returns>
        public static bool operator !=(BaseEntity x, BaseEntity y)
        {
            return !object.Equals(x, y);
        }

        #endregion
    }
}

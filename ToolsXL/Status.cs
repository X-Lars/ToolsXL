using System;

namespace ToolsXL
{
    /// <summary>
    /// Extends a flags enumeration with operators for addition, substraction and equasion of flags.
    /// </summary>
    /// <typeparam name="T">A <see cref="T"/> specifying the type of enumeration to uses.</typeparam>
    /// <remarks><i>The specified <typeparamref name="T"/> has to be decorated with the [Flags] attribute.</i></remarks>
    public class Status<T> where T: Enum
    {
        #region Fields

        /// <summary>
        /// Stores the status flags.
        /// </summary>
        private T _Flags;

        #endregion

        #region Events

        /// <summary>
        /// Provides the signature for the <see cref="Changed"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"/> that raised the event.</param>
        /// <param name="e">A <see cref="StatusChangeEventArgs{T}"/> containing event data.</param>
        public delegate void StatusChangedEventHandler(object sender, StatusChangeEventArgs<T> e);

        /// <summary>
        /// Event raised when the <see cref="Flags"/> property is changed.
        /// </summary>
        public event StatusChangedEventHandler Changed;

        #endregion

        #region Constructor

        /// <summary>
        /// Creates and initializes a new instance of the <see cref="Status{T}"/> class with a default <typeparamref name="T"/>.
        /// </summary>
        public Status() : this(default(T)) { }

        /// <summary>
        /// Creates and initializes a new instance of the <see cref="Status{T}"/> class.
        /// </summary>
        /// <param name="flags">An <see cref="Enum"/> decorated with the [Flags] attribute.</param>
        /// <exception cref="StatusException">When the provided <see cref="Enum"/> is not decorated with the [Flags] attribute.</exception>
        public Status(T flags) 
        {
            if (!typeof(T).IsDefined(typeof(FlagsAttribute), false))
                throw new StatusException($"{typeof(T).Name} has to be decorated with the [Flags] attribute to be used as status.");

            Flags = flags; 
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the status flags.
        /// </summary>
        public T Flags
        {
            get { return _Flags; }
            private set
            {
                _Flags = value;

                Changed?.Invoke(this, new StatusChangeEventArgs<T>(_Flags));
            }
        }

        #endregion

        #region Operator Overloading

        /// <summary>
        /// Overloads the assignment operator to create a new <see cref="Status{T}"/> initialized with the provided <typeparamref name="T"/>.
        /// </summary>
        /// <param name="flags">A <typeparamref name="T"/> to initialize the <see cref="Status{T}"/>.</param>
        public static implicit operator Status<T>(T flags)
        {
            return new Status<T>(flags);
        }

        /// <summary>
        /// Overloads the assignment operator to convert the <see cref="Status{T}"/> to <typeparamref name="T"/>.
        /// </summary>
        /// <param name="status">A <see cref="Status{T}"/> to convert to <typeparamref name="T"/>.</param>.
        public static implicit operator T(Status<T> status)
        {
            return status.Flags;
        }

        /// <summary>
        /// Overloads the addition operator to add a <typeparamref name="T"/> value to the <see cref="Status{T}.Flags"/>.
        /// </summary>
        /// <param name="status">The LHS <see cref="Status{T}"/> to add the specified flag to.</param>
        /// <param name="flags">The RHS <typeparamref name="T"/> to add.</param>
        /// <returns>The <see cref="Status{T}"/> with the specified flag set.</returns>
        public static Status<T> operator +(Status<T> status, T flags)
        {
            
            int lhs = Convert.ToInt32(status.Flags);
            int rhs = Convert.ToInt32(flags);

            lhs |= rhs;

            status.Flags = (T)Enum.ToObject(typeof(T), lhs);

            return status;
        }

        /// <summary>
        /// Overloads the substraction operator to remove a <typeparamref name="T"/> value from the <see cref="Status{T}.Flags"/>.
        /// </summary>
        /// <param name="status">The LHS <see cref="Status{T}"/> to remove the specified flag from.</param>
        /// <param name="flags">The RHS <see cref="T"/> to remove.</param>
        /// <returns>The <see cref="Status{T}"/> with the specified flag cleared.</returns>
        public static Status<T> operator -(Status<T> status, T flags)
        {
            int lhs = Convert.ToInt32(status.Flags);
            int rhs = Convert.ToInt32(flags);

            lhs &= ~rhs;

            status.Flags = (T)Enum.ToObject(typeof(T), lhs);

            return status;
        }

        /// <summary>
        /// Overloads the equals operator to compare the <see cref="Status{T}"/> with a <typeparamref name="T"/>.
        /// </summary>
        /// <param name="status">The LHS <see cref="Status{T}"/> to compare.</param>
        /// <param name="flags">The RHS <typeparamref name="T"/> to compare to.</param>
        /// <returns>A <see cref="bool"/> containing true if the specified status flag is set, false otherwise.</returns>
        public static bool operator ==(Status<T> status, T flags)
        {

            int lhs = Convert.ToInt32(status.Flags);
            int rhs = Convert.ToInt32(flags);

            if (rhs == 0)
            {
                return lhs == 0;
            }

            return status.Flags.HasFlag(flags);
        }

        /// <summary>
        /// Overloads the not equals operator to compare the <see cref="Status{T}"/> with a <typeparamref name="T"/>.
        /// </summary>
        /// <param name="status">The LHS <see cref="Status{T}"/> to compare.</param>
        /// <param name="flags">The RHS <typeparamref name="T"/> to compare to.</param>
        /// <returns>A <see cref="bool"/> containing false if the specified status flag is set, false otherwise.</returns>
        public static bool operator !=(Status<T> status, T flags)
        {
            int lhs = Convert.ToInt32(status.Flags);
            int rhs = Convert.ToInt32(flags);

            if (rhs == 0)
                return lhs != 0;

            return !status.Flags.HasFlag(flags);
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Determines whether the specified object is equal to the <see cref="Status{T}"/> object.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to compare the <see cref="Status{T}"/> object to.</param>
        /// <returns>A <see cref="bool"/> containing true if the specified object is equal to the <see cref="Status{T}"/> object, false otherwise.</returns>
        /// <remarks><i>Not overridden, used to suppress operator overloading warning.</i></remarks>
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <summary>
        /// Creates a hash code for the <see cref="Status{T}"/> object.
        /// </summary>
        /// <returns>An <see cref="int"/> containing the hash code for the <see cref="Status{T}"/> object.</returns>
        /// <remarks><i>Not overridden, used to suppress operator overloading warning.</i></remarks>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }

    /// <summary>
    /// Extends an <see cref="EventArgs"/> with a <see cref="Status"/> property.
    /// </summary>
    /// <typeparam name="T">A <typeparamref name="T"/> defining the type of the status.</typeparam>
    public class StatusChangeEventArgs<T> : EventArgs
    {
        /// <summary>
        /// Creates and initializes new instance of the <see cref="StatusChangeEventArgs{T}"/> class.
        /// </summary>
        /// <param name="status">A <typeparamref name="T"/> containing the status to provide through the event.</param>
        public StatusChangeEventArgs(T status)
        {
            Status = status;
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        public T Status { get;}
    }

    /// <summary>
    /// Defines a <see cref="Status{T}"/> specific exception.
    /// </summary>
    public class StatusException : Exception
    {
        /// <summary>
        /// Creates and initializes a new instance of the <see cref="StatusException"/> class.
        /// </summary>
        /// <param name="message">A <see cref="string"/> containing the message to associate with the exception.</param>
        public StatusException(string message) : base(message) { }
    }
}

using System;

namespace OzekiDemoSoftphone.PM
{
    /// <summary>
    /// Generic Event Args.
    /// </summary>
    /// <typeparam name="T">The type of object.</typeparam>
    /// <remarks>
    /// The GEventArgs class contains an object of arbitrary types.
    /// It is useful when someone needs to emit events of arbitrary types, without writing
    /// event classes for every types.
    /// </remarks>
    public sealed class GEventArgs<T> : EventArgs
    {
        /// <summary>
        /// The item itself.
        /// </summary>
        public T Item { get; private set; }

        /// <summary>
        /// Constructs a GEventArgs object.
        /// </summary>
        /// <param name="item">The type of item.</param>
        public GEventArgs(T item)
        {
            Item = item;
        }
    }
}

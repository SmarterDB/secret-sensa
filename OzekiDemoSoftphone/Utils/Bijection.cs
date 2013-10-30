using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace OzekiDemoSoftphone.Utils
{
    /// <summary>
    /// Bijection in mathematical sense.
    /// </summary>
    /// <remarks>
    /// The bijection holds mapping between objects. 
    /// If you want to know more please visit the
    /// http://en.wikipedia.org/wiki/Bijection page.
    /// </remarks>
    /// <typeparam name="K">The first type parameter.</typeparam>
    /// <typeparam name="V">The second type parameter.</typeparam>
    class Bijection<K,V> : IDictionary<K, V>, INotifyPropertyChanged where K:class
                                                                     where V:class 
    {
        Dictionary<K, V> Pairs;
        Dictionary<V, K> IPairs;
        object sync;
        public Bijection()
        {
            Pairs = new Dictionary<K, V>();
            IPairs = new Dictionary<V, K>();

            sync = new object();
        }

        /// <summary>
        /// Get the associated element.
        /// </summary>
        /// <param name="k">The given element.</param>
        /// <returns>The associated element.</returns>
        public V Get(K k)
        {
            lock (sync)
            {
                if(ContainsKey(k))
                    return Pairs[k];

                return null;
            }

        }

        /// <summary>
        /// Get the associated element.
        /// </summary>
        /// <param name="v">The given element.</param>
        /// <returns>The associated element.</returns>
        public K GetInv(V v)
        {
            lock (sync)
            {
                if (ContainsInv(v))
                    return IPairs[v];

                return null;
            }

        }

        /// <summary>
        /// Insert two elements into the bijection.
        /// </summary>
        /// <param name="k">First element.</param>
        /// <param name="v">Second element.</param>
        public void AddBijection(K k, V v)
        {
            lock (sync)
            {
                if (Pairs.ContainsKey(k) || IPairs.ContainsKey(v))
                    throw new ArgumentException("Key or value is already contained!");

                Pairs.Add(k, v);
                IPairs.Add(v, k);

                if (PropertyChanged == null)
                    return;

                PropertyChanged(this, new PropertyChangedEventArgs("AddBijection"));
            }

        }

        /// <summary>
        /// Remove association from the bijection.
        /// </summary>
        /// <param name="k">First element of the association.</param>
        /// <returns>Returns true if the remove was successful, otherwise false.</returns>
        public bool RemoveBijection(K k)
        {
            lock (sync)
            {
                if (!Pairs.ContainsKey(k))
                    return false;

                V v = Pairs[k];
                bool p = IPairs.Remove(v);
                if (!p) return false;
                bool i = Pairs.Remove(k);
                if (!i)
                {
                    IPairs.Add(v, k);
                    return false;
                }

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("RemoveBijection"));

                return true;
            }

        }

        /// <summary>
        /// Remove association from the bijection.
        /// </summary>
        /// <param name="v">Second element of the association.</param>
        /// <returns>Returns true if the remove was successful, otherwise false.</returns>
        public bool RemoveInv(V v)
        {
            lock (sync)
            {
                if (!IPairs.ContainsKey(v))
                    return false;

                K k = IPairs[v];
                bool p = IPairs.Remove(v);
                if (!p) return false;
                bool i = Pairs.Remove(k);
                if (!i)
                {
                    IPairs.Add(v, k);
                    return false;
                }

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("RemoveBijection"));

                return true;
            }

        }

        /// <summary>
        /// The bijection contains the key?
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Returns true if the key is contained, otherwise false.</returns>
        public bool ContainsKey(K key)
        {
            lock (sync)
            {
                return Pairs.ContainsKey(key);
            }

        }

        /// <summary>
        /// The bijection contains the key?
        /// </summary>
        /// <param name="val">The key.</param>
        /// <returns>Returns true if the key is contained, otherwise false.</returns>
        public bool ContainsInv(V val)
        {
            lock (sync)
            {
                return IPairs.ContainsKey(val);
            }

        }

        /// <summary>
        /// Returns the collection of K values.
        /// </summary>
        public ICollection<K> Keys
        {
            get
            {
                lock (sync)
                {
                    return Pairs.Keys;
                }

            }
        }

        /// <summary>
        /// Removes the pair represented by the K value
        /// </summary>
        /// <param name="key">The K value.</param>
        /// <returns>True if the removing was succesful.</returns>
        bool IDictionary<K, V>.Remove(K key)
        {
            lock (sync)
            {
                return this.RemoveBijection(key);
            }

        }

        /// <summary>
        /// Tries to get value from the bijection.
        /// </summary>
        /// <param name="key">The K key value.</param>
        /// <param name="value">The assciated V value.</param>
        /// <returns>Returns true if the association is in the bijection.</returns>
        public bool TryGetValue(K key, out V value)
        {
            lock (sync)
            {
                return Pairs.TryGetValue(key, out value);
            }

        }

        /// <summary>
        /// Reteurns the collection of V values.
        /// </summary>
        public ICollection<V> Values
        {
            get
            {
                lock (sync)
                {
                    return Pairs.Values; 
                }

            }
        }

        /// <summary>
        /// Gets or sets a value assicated with the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public V this[K key]
        {
            get
            {
                lock (sync)
                {
                    return Pairs[key]; 
                }

            }
            set
            {
                lock (sync)
                {
                    this.AddBijection(key, value);
                }

            }
        }

        /// <summary>
        /// Add a pair into the bijection.
        /// </summary>
        /// <param name="item">The key value pair.</param>
        public void Add(KeyValuePair<K, V> item)
        {
            lock (sync)
            {
                this.AddBijection(item.Key, item.Value);
            }

        }

        /// <summary>
        /// Clear the bijection.
        /// </summary>
        public void Clear()
        {
            lock (sync)
            {
                Pairs.Clear();
                IPairs.Clear();
            }

        }

        /// <summary>
        /// Searches a key value pair in the bijection.
        /// </summary>
        /// <param name="item">The key value pair.</param>
        /// <returns>Returns true if the bijection contains the key-value pair.</returns>
        public bool Contains(KeyValuePair<K, V> item)
        {
            lock (sync)
            {
                return Pairs.ContainsKey(item.Key) && IPairs.ContainsKey(item.Value);
            }

        }

        /// <summary>
        /// Not implemented yet.
        /// </summary>
        /// <param name="array">The result array.</param>
        /// <param name="arrayIndex">Start index of the copy.</param>
        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the number of elements in bijection.
        /// </summary>
        public int Count
        {
            get
            {
                lock(sync)
                    return Pairs.Count;
            }
        }

        /// <summary>
        /// Always returns false.
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Removes a key value pair from the bijection.
        /// </summary>
        /// <param name="item">The key value pair.</param>
        /// <returns>Returns true if the removing was successful</returns>
        public bool Remove(KeyValuePair<K, V> item)
        {
            lock (sync)
            {
                return RemoveBijection(item.Key);
            }
           
        }

        /// <summary>
        /// Returns a Key-Value pair enumerator.
        /// </summary>
        /// <returns>Returns a Key-Value pair enumerator.</returns>
        public IEnumerator<KeyValuePair<K, V>> GetEnumerator()
        {
            lock (sync)
            {
                return Pairs.GetEnumerator();
            }

        }

        /// <summary>
        /// Returns a Key-Value pair enumerator.
        /// </summary>
        /// <returns>Returns a Key-Value pair enumerator.</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            lock (sync)
            {
                return Pairs.GetEnumerator();
            }

        }

        /// <summary>
        /// Add a Key Value pair into the bijection.
        /// </summary>
        /// <param name="key">The key object.</param>
        /// <param name="value">The value object.</param>
        public void Add(K key, V value)
        {
            lock (sync)
            {
                AddBijection(key, value);
            }
   
        }

        /// <summary>
        /// Emit when the bijection has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

    }
}

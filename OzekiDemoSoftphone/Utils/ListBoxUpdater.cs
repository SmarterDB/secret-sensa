using System.Collections.Generic;
using System.Windows.Forms;

namespace OzekiDemoSoftphone.Utils
{
    /// <summary>
    /// Utils to handle information change in displayed listbox.
    /// </summary>
    static class ListBoxUpdater
    {
        /// <summary>
        /// Update infromation from the given bijection.
        /// </summary>
        /// <param name="listBox">The listbox to update.</param>
        /// <param name="b">The data source bijection.</param>
        /// <typeparam name="K">The first type param of the given bijection.</typeparam>
        /// <typeparam name="V">The second type param of the given bijection.</typeparam>
        public static void UpdateFromBijection<K, V>(this ListBox listBox, Bijection<K, V> b)
                                                     where K:class 
                                                     where V:class 
        {
            /* Close the drawing of listbox, begin update.
             * */
            listBox.BeginUpdate();
            bool wasEmpty = listBox.Items.Count == 0;
            object selectedItem = listBox.SelectedItem;
            bool contains = false;
            /* Clear every element from listbox.
             * */
            listBox.Items.Clear();
            foreach (var kvp in b)
            {
                /* Add every element from the bijection.
                 * */
                listBox.Items.Add(kvp.Key);
                /* Check that the element equals to the one selected.
                 * */
                if (kvp.Key.Equals(selectedItem))
                    contains = true;
            }
            /* If the refilled listbox contains the selected element, we select it again.
             */
            if (contains)
                listBox.SelectedItem = selectedItem;
            /* Draw the listbox, end update.
             * */
            listBox.EndUpdate();
        }

        /// <summary>
        /// Update information from the given enumeration.
        /// </summary>
        /// <param name="listBox">The listbox to update.</param>
        /// <param name="ts">The enumeration.</param>
        /// <typeparam name="T">The type parameter of the enumeration.</typeparam>
        public static void UpdateFromIEnumerable<T>(this ListBox listBox, IEnumerable<T> ts)
        {
            /* Close the drawing of listbox, begin update.
             * */
            listBox.BeginUpdate();
            bool wasEmpty = listBox.Items.Count == 0;
            object selectedItem = listBox.SelectedItem;
            bool contains = false;
            /* Clear every element from listbox.
             * */
            listBox.Items.Clear();
            foreach (T t in ts)
            {
                /* Add every element from the enumeration.
                 * */
                listBox.Items.Add(t);
                /* Check that the element equals to the one selected.
                 * */
                if (t.Equals(selectedItem))
                    contains = true;
            }
            /* If the refilled listbox contains the selected element, we select it again.
             */
            if (contains)
                listBox.SelectedItem = selectedItem;
            /* Draw the listbox, end update.
             * */
            listBox.EndUpdate();
        }
    }
}

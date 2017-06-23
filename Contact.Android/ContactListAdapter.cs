using System;
using System.Collections.Generic;
using Android.Widget;
using Android.Support.V7.Widget;

namespace Contact.Droid
{
    class ContactListAdapter <T>
    {
        List<T> contacts;
        RecyclerView.Adapter cAdapter;

        public ContactListAdapter()
        {
            contacts = new List<T>();
        }

        public RecyclerView.Adapter Adapter
        {
            get {  return cAdapter; }
            set { cAdapter = value; }
        }

        public void Add(T item)
        {
            contacts.Add(item);
            if (Adapter != null){
                Adapter.NotifyItemInserted(0);
            }
        }

        public void Remove(int position)
        {
            contacts.RemoveAt(position);
            if (Adapter != null){
                Adapter.NotifyItemRemoved(0);
            }
        }

        public T this[int index]
        {
            get { return contacts[index]; }
            set { contacts[index] = value; }
        }

        public int Count
        {
            get { return contacts.Count; }
        }
    }
}
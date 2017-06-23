using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.Widget;
using System.Collections.Generic;

namespace Contact.Droid
{
	[Activity (Label = "Contacts", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/Theme.DesignDemo")]
	public class MainActivity : Activity
	{
        private RecyclerView recyclerview;
        RecyclerView.LayoutManager recyclerview_layoutmanger;
        private RecyclerView.Adapter recyclerview_adapter;
        private ContactListAdapter<ContactList> contactListitems;
        List<ContactList> contact;
        private SearchView sv;
        string[] names = new string[] {"Darshan","Darshit", "Darpan", "Kandarp" , "Akash" , "Monil" , "Gaurav" };

        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
			SetContentView (Resource.Layout.Main);
        
            recyclerview = FindViewById<RecyclerView>(Resource.Id.recyclerview);
            contact = new List<ContactList>();
            contactListitems = new ContactListAdapter<ContactList>();
            recyclerview_layoutmanger = new LinearLayoutManager(this, LinearLayoutManager.Vertical, false);
            recyclerview.SetLayoutManager(recyclerview_layoutmanger);

            for (int i = 0; i < names.Length; i++)
            {
                contact.Add(new ContactList { ContactName = names[i] });
            }

            foreach (var s in contact)
            {
                contactListitems.Add(s);
            }

            recyclerview_adapter = new RecyclerAdapter(contactListitems);
            recyclerview.SetAdapter(recyclerview_adapter);

            sv = FindViewById<SearchView>(Resource.Id.searchview);

            sv.QueryTextChange += Sv_QueryTextChange;
        }

        private void Sv_QueryTextChange(object sender, SearchView.QueryTextChangeEventArgs e)
        {
            ContactListAdapter<ContactList> contactListit = new ContactListAdapter<ContactList>();
            List<ContactList> fcontact = new List<ContactList>();

            for (int i=0;i<names.Length;i++)
            {
                if (names[i].Contains(e.NewText))
                {
                    fcontact.Add(new ContactList { ContactName = names[i]});
                }
            }

            foreach(var c in fcontact)
            {
                contactListit.Add(c);
            }

            recyclerview_adapter = new RecyclerAdapter(contactListit);
            recyclerview.SetAdapter(recyclerview_adapter);
        }

        class RecyclerAdapter : RecyclerView.Adapter
        {
            private ContactListAdapter<ContactList> Mitems;

            public RecyclerAdapter(ContactListAdapter<ContactList> Mitems)
            {
                this.Mitems = Mitems;
                NotifyDataSetChanged();
            }

            public class MyView : RecyclerView.ViewHolder
            {
                public View mainview { get; set; }
                public TextView mtxtcontactname { get; set; }
                public MyView(View view) : base(view)
                {
                    mainview = view;
                }
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                View listitem = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.ContactLayout, parent, false);
                TextView txtcontactname = listitem.FindViewById<TextView>(Resource.Id.txtcontactname);
                MyView view = new MyView(listitem)
                {
                    mtxtcontactname = txtcontactname,
                };
                return view;
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                MyView myholder = holder as MyView;
                myholder.mtxtcontactname.Text = Mitems[position].ContactName;
            }

            public override int ItemCount
            {
                get { return Mitems.Count; }
            }
        }
    }
}



using System;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using App1.ViewModel;
using Android.Support.V4.Widget;
using Android.App;
using Android.Content;
using App1.Droid.Activities;

namespace App1.Droid {
    public class MyItemsFragment : Android.Support.V4.App.Fragment, Fragments.IFragmentVisible {

        public static MyItemsFragment NewInstance() =>
            new MyItemsFragment { Arguments = new Bundle() };

        MyItemsAdapter adapter;
        SwipeRefreshLayout refresher;
        ProgressBar progress;

        public MyItemsViewModel ViewModel {
            get;
            set;
        }

        public override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
            ViewModel = new MyItemsViewModel();

            View view = inflater.Inflate(Resource.Layout.fragment_myitems, container, false);
            var recyclerView =
                view.FindViewById<RecyclerView>(Resource.Id.recyclerView);

            recyclerView.HasFixedSize = true;
            recyclerView.SetAdapter(adapter = new MyItemsAdapter(Activity, ViewModel));
            refresher = view.FindViewById<SwipeRefreshLayout>(Resource.Id.refresher);

            refresher.SetColorSchemeColors(Resource.Color.accent);

            progress = view.FindViewById<ProgressBar>(Resource.Id.progressbar_loading);
            progress.Visibility = ViewStates.Gone;
            return view;
        }

        public override void OnStart() {
            base.OnStart();

            refresher.Refresh += Refresher_Refresh;
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            adapter.ItemClick += Adapter_ItemClick;
            BecameVisible();
        }


        public override void OnStop() {
            base.OnStop();
            refresher.Refresh -= Refresher_Refresh;
            ViewModel.PropertyChanged -= ViewModel_PropertyChanged;
            adapter.ItemClick -= Adapter_ItemClick;
        }

        private void Adapter_ItemClick(object sender, RecyclerClickEventArgs e) {
            var item = ViewModel.Items[e.Position];
            var intent = new Intent(Activity, typeof(MyItemDetailActivity));

            intent.PutExtra("data", Newtonsoft.Json.JsonConvert.SerializeObject(item));
            Activity.StartActivity(intent);
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            Activity.RunOnUiThread(() => {
                switch (e.PropertyName) {
                    case nameof(ViewModel.IsBusy): {
                            refresher.Refreshing = ViewModel.IsBusy;
                            progress.Visibility = ViewModel.IsBusy ? ViewStates.Visible : ViewStates.Gone;
                        }
                        break;
                }
            });
        }

        private void Refresher_Refresh(object sender, EventArgs e) {
            ViewModel.LoadItemsCommand.Execute(null);
        }

        public void BecameVisible() {
            if (ViewModel.Items.Count == 0 || MyItemsViewModel.IsDirty)
                ViewModel.LoadItemsCommand.Execute(null);
        }
    }

    class MyItemsAdapter : BaseRecycleViewAdapter {
        MyItemsViewModel viewModel;
        Activity activity;
        public MyItemsAdapter(Activity activity, MyItemsViewModel viewModel) {
            this.activity = activity;
            this.viewModel = viewModel;
            this.viewModel.Items.CollectionChanged += (sender, args) => {
                this.activity.RunOnUiThread(NotifyDataSetChanged);
            };
        }

        // Create new views (invoked by the layout manager)
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType) {
            //Setup your layout here
            View itemView = null;
            var id = Resource.Layout.item_my_item;
            itemView = LayoutInflater.From(parent.Context).Inflate(id, parent, false);

            var vh = new MyItemsViewHolder(itemView, OnClick, OnLongClick);
            return vh;
        }

        // Replace the contents of a view (invoked by the layout manager)
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position) {
            var item = viewModel.Items[position];

            // Replace the contents of the view with that element
            var myHolder = holder as MyItemsViewHolder;
            myHolder.TextView.Text = item.Text;
            myHolder.DetailTextView.Text = item.Description;
            myHolder.QuantityTextView.Text = item.Quantity.ToString();
            //TODO: Setup image here.
        }

        public override int ItemCount => viewModel.Items.Count;

    }

    public class MyItemsViewHolder : RecyclerView.ViewHolder {
        public TextView TextView { get; set; }
        public TextView DetailTextView { get; set; }
        public TextView QuantityTextView { get; set; }

        public MyItemsViewHolder(View itemView, Action<RecyclerClickEventArgs> clickListener,
                            Action<RecyclerClickEventArgs> longClickListener) : base(itemView) {
            TextView = itemView.FindViewById<TextView>(Android.Resource.Id.Text1);
            DetailTextView = itemView.FindViewById<TextView>(Android.Resource.Id.Text2);
            QuantityTextView = itemView.FindViewById<TextView>(Resource.Id.item_count);
            itemView.Click += (sender, e) => clickListener(new RecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
            itemView.LongClick += (sender, e) => longClickListener(new RecyclerClickEventArgs { View = itemView, Position = AdapterPosition });
        }
    }

}


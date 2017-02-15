using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App1.Model;
using Android.Support.Design.Widget;
using App1.ViewModel;
using Android.Support.V4.App;

namespace App1.Droid.Activities {
    [Activity(Label = "Details", ParentActivity = typeof(MainActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = ".MainActivity")]
    public class BrowseItemDetailActivity : BaseActivity {
        /// <summary>
        /// Specify the layout to inflace
        /// </summary>
        protected override int LayoutResource => Resource.Layout.activity_item_details;


        FloatingActionButton saveButton;
        ItemDetailViewModel viewModel;
        Spinner spinner;
        ProgressBar progress;
        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            var data = Intent.GetStringExtra("data");

            var item = Newtonsoft.Json.JsonConvert.DeserializeObject<Item>(data);
            viewModel = new ItemDetailViewModel(item);
            spinner = FindViewById<Spinner>(Resource.Id.spinner);

            var adapter = new ArrayAdapter<string>(this,
                Android.Resource.Layout.SimpleSpinnerItem,
                new[] { "1", "2", "3" });

            adapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            spinner.Adapter = adapter;


            FindViewById<TextView>(Resource.Id.description).Text = item.Description;

            progress = FindViewById<ProgressBar>(Resource.Id.progressbar_loading);
            saveButton = FindViewById<FloatingActionButton>(Resource.Id.save_button);

            viewModel.OnFinished = (i) => {
                //Item has been saved
                Finish();
            };


            SupportActionBar.Title = item.Text;

            progress.Visibility = ViewStates.Gone;

        }

        protected override void OnStart() {
            base.OnStart();
            saveButton.Click += SaveButton_Click;
            viewModel.PropertyChanged += ViewModel_PropertyChanged;

        }


        protected override void OnStop() {
            base.OnStop();
            saveButton.Click -= SaveButton_Click;
            viewModel.PropertyChanged -= ViewModel_PropertyChanged;
        }


        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) {
            RunOnUiThread(() => {
                switch (e.PropertyName) {
                    case nameof(viewModel.IsBusy): {
                            saveButton.Enabled = !viewModel.IsBusy;
                            progress.Visibility = viewModel.IsBusy ? ViewStates.Visible : ViewStates.Gone;
                        }
                        break;
                }
            });
        }

        private void SaveButton_Click(object sender, EventArgs e) {
            viewModel.Quantity = spinner.SelectedItemPosition + 1;
            viewModel.SaveCommand.Execute(null);
        }

        public override bool OnOptionsItemSelected(IMenuItem item) {
            switch (item.ItemId) {
                case Android.Resource.Id.Home:

                    var intent = NavUtils.GetParentActivityIntent(this);
                    if (NavUtils.ShouldUpRecreateTask(this, intent)) {
                        //This activity is not part of the app's tasks, so create new when navigating
                        Android.Support.V4.App.TaskStackBuilder.Create(this).
                            AddNextIntentWithParentStack(intent)
                            .StartActivities();
                    } else {
                        NavUtils.NavigateUpTo(this, intent);
                    }
                    break;
            }

            return base.OnOptionsItemSelected(item);
        }

    }
}
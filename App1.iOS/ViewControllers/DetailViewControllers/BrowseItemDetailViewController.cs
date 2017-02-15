using System;
using UIKit;

using App1.ViewModel;

namespace App1.iOS
{
    public partial class BrowseItemDetailViewController : UIViewController
    {
		public ItemDetailViewModel ViewModel { get; set; }
		public BrowseItemDetailViewController(IntPtr handle) : base(handle) { }

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			Title = ViewModel.Title;
			ItemNameLabel.Text = ViewModel.Item.Text;
			ItemDescriptionLabel.Text = ViewModel.Item.Description;
			QuantityAmountLabel.Text = ViewModel.Quantity.ToString();
		}

		partial void AddToItemsLabel_TouchUpInside(UIButton sender)
		{
			ViewModel.SaveCommand.Execute(null);
		}

		partial void QuantityStepper_ValueChanged(UIStepper sender)
		{
			var stepper = sender;
			ViewModel.Quantity = (int)stepper.Value;
			QuantityAmountLabel.Text = ViewModel.Quantity.ToString();
		}
    }
}
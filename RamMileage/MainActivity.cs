using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace RamMileage
{
	[Activity(Label = "RamMileage", MainLauncher = true, Icon = "@mipmap/icon")]
	public class MainActivity : Activity
	{
		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			EditText distanceText = FindViewById<EditText>(Resource.Id.DistanceText);
			EditText mileageText = FindViewById<EditText>(Resource.Id.MileageText);
			EditText timeText = FindViewById<EditText>(Resource.Id.TimeText);
			EditText fuelText = FindViewById<EditText>(Resource.Id.FuelText);
			Button postButton = FindViewById<Button>(Resource.Id.PostButton);

			string stringToPost = string.Empty;

			postButton.Click += (object sender, EventArgs e) =>
			{
				//Create the https string to post the data
				stringToPost = RamMileage.MileagePostString.ToPostString(distanceText.Text, mileageText.Text, timeText.Text, fuelText.Text);

				//Post the string to Thingspeak

			};

		}
	}
}



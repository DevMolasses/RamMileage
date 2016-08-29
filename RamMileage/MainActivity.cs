using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Net;
using System.IO;
using System.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RamMileage
{
	[Activity(Label = "Ram Mileage", MainLauncher = true, Icon = "@mipmap/ramlauncher")]
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


			postButton.Click += async (sender, e) =>
			{
				//Create the https string to post the data
				string url = RamMileage.MileagePostString.ToPostString(distanceText.Text, mileageText.Text, timeText.Text, fuelText.Text);

				//Post the string to Thingspeak
				JsonValue json = await PostToThingspeak(url);

				//Parse return channel and post ID
				ParseAndDisplay(json);

			};

		}

		private async Task<JsonValue> PostToThingspeak(string url)
		{
			//Create an HTTP web request using the URL:
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
			request.ContentType = "application/json";
			request.Method = "POST";

			// Send the request to the server and wait for the response:
			using (WebResponse response = await request.GetResponseAsync())
			{
				using (Stream stream = response.GetResponseStream())
				{
					JsonValue jsonDoc = await Task.Run(() => JsonValue.Load(stream));
					Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

					return jsonDoc;
				}
			}
		}

		private void ParseAndDisplay(JsonValue json)
		{
			TextView channel = FindViewById<TextView>(Resource.Id.channelText);
			TextView entry = FindViewById<TextView>(Resource.Id.entryText);

			try
			{
				string jstr = json.ToString();
				//jstr = "{\"data\":[" + jstr + "]}";

				var jnet = Newtonsoft.Json.Linq.JObject.Parse(jstr);

				channel.Text = jnet["channel_id"].ToString();
				entry.Text = jnet["entry_id"].ToString();
			}
			catch (Exception)
			{
				channel.Text = "Upload Error";
				entry.Text = "";
			}
		}
	}
}



using System;
using System.Text;
namespace RamMileage
{
	public static class MileagePostString
	{
		public static string ToPostString(string distance, string mileage, string time, string fuel)
		{
			//string channelID = "148709";
			string writeKey = "VTY04EOQ2CDVSKDQ";

			return string.Format("https://api.thingspeak.com/update.json?api_key={0}&field1={1}&field2={2}&field3={3}&field4={4}", writeKey, distance, mileage, time, fuel);
		}
	}
}


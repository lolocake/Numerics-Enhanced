using System;
using NUnit.Framework;
using System.Linq;

namespace Orbifold.Numerics.Tests
{

	public enum Outlook
	{
		Sunny,
		Overcast,
		Rainy
	}

	public enum Temperature
	{
		Hot,
		Mild,
		Cool
	}

	public enum Humidity
	{
		High,
		Normal
	}

	public class Tennis
	{
		[Feature]
		public Outlook Outlook { get; set; }

		[Feature]
		public Temperature Temperature { get; set; }

		[Feature]
		public Humidity Humidity { get; set; }

		[Feature]
		public bool Windy { get; set; }

		[Label]
		public bool Play { get; set; }

		public static Tennis Make(Outlook outlook, Temperature temperature, Humidity humidity, bool windy, bool play)
		{
			return new Tennis {
				Outlook = outlook,
				Temperature = temperature,
				Humidity = humidity,
				Windy = windy,
				Play = play
			};
		}

		public static Tennis[] GetData()
		{
			return new [] {
				Tennis.Make(Outlook.Sunny, Temperature.Hot, Humidity.High, false, false),
				Tennis.Make(Outlook.Sunny, Temperature.Hot, Humidity.High, true, false),
				Tennis.Make(Outlook.Over
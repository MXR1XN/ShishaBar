using System; 

namespace Assets.Scripts
{
	public class Shisha 
	{
		public static readonly string[] ShishaName = { "Blue Mist", "Adalya Love 66", "Mazaya" };
		public static readonly string[] ShishaFlavor = { "Apple", "Starbuzz", "Strawberry" };
		private static int _LAST_ID = 0;
		private int _id;
		public bool isReady;

		public string Name { get; private set; }
		public string Flavor { get; private set; }
		public int Strenght { get; private set; }

		private Random _random = new Random();
		public Shisha() 
		{
			Name = ShishaName[_random.Next(0, ShishaName.Length - 1)];
			Flavor = ShishaFlavor[_random.Next(0, ShishaFlavor.Length - 1)];
			Strenght = _random.Next(0, 5);
			_id = _LAST_ID++;
		}
	

	}
}

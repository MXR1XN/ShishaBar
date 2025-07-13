using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
	public static class Orders 
	{
		public static HashSet<Shisha> OrderedShishas = new HashSet<Shisha>();
		public static HashSet<Shisha> CompleteShishas = new HashSet<Shisha>();
	}
/*	public class Order 
	{
		
		public int _id;
		public int _seatId;
		public Shisha _shisha;

		public Order(int chairId ,int seatId, Shisha shisha) 
		{
			_id = chairId;
			_seatId = seatId;
			_shisha = shisha;
		}

	}*/
}

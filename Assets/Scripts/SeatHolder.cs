using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
	public class SeatHolder : MonoBehaviour, IPlaceHolder<IAvailablePlace>
	{
		[SerializeField] List<Seat> seats;
		void Awake() 
		{
			seats = GetComponentsInChildren<Seat>().ToList();
		}
		public IAvailablePlace GetAvailable(Customer customer)
		{
			var availableSeat = seats.Where(c => c.IsAvailable).FirstOrDefault();
			return availableSeat.TakePlace(customer);
		}
		public bool HasAvailableChairs()
		{
			return seats.Any(seat => seat.IsAvailable);
		}
	}
}

using UnityEngine;

namespace Assets.Scripts
{
	public interface IAvailablePlace : ITransform
	{
		bool IsAvailable { get;}
		int ID { get;}
		IAvailablePlace TakePlace(Customer customer);
		void ReleasePlace();
		Transform SeatPoint {get;}
		Transform LookPoint {get;}
	}
}

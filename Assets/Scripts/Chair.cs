using Assets.Scripts;
using UnityEngine;

public class Chair : MonoBehaviour, IAvailablePlace, ITransform
{
	[SerializeField] private Transform seatPoint;
	[SerializeField] private bool isAvailable;
	[SerializeField] private Transform lookPoint;
	public int ID => id;
	private int id;

	private static int currentID = 0;
	public Transform LookPoint => lookPoint;
	public bool IsAvailable 
	{ 
		get => isAvailable;
		set => isAvailable = value; 
	}

	public Transform Transform => transform;

	public Transform SeatPoint => seatPoint;

	private void Awake()
	{

		id = currentID++;
	}

	public void ReleasePlace()
	{
		IsAvailable = true;
	}

	public IAvailablePlace TakePlace(Customer customer)
	{
		IsAvailable = false;
		return this;
	}

	public static int GetCountChair() 
	{
		return currentID;
	}
}

using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
	[SerializeField] private Customer customerPrefab; 
	[SerializeField] private SeatHolder seatHolder; 
	[SerializeField] private Transform spawnPoint;
	[SerializeField] private float minSpawnTime = 3f; 
	[SerializeField] private float maxSpawnTime = 8f;
	[SerializeField] private int maxCustomers = 30;

	public static int PlaceCount;
	public int MaxPlaces;
	private List<Customer> activeCustomers = new List<Customer>();
	private Coroutine spawnCoroutine;

	private Chair chair;

	private void Start()
	{
		StartCoroutine(SpawnCustomers());

	}

	private IEnumerator SpawnCustomers()
	{
		
		while (true)
		{
			if (activeCustomers.Count < maxCustomers && PlaceCount < Chair.GetCountChair())
			{
				float waitTime = Random.Range(minSpawnTime, maxSpawnTime);
				yield return new WaitForSeconds(waitTime);

				SpawnCustomer();
			}
			else
			{
				yield return new WaitForSeconds(1f);
			}
		}
	}

	private void SpawnCustomer()
	{
		Customer customer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);

		if (customer != null)
		{
			customer.SeatHolder = seatHolder;
			activeCustomers.Add(customer);
			PlaceCount++;
		}
	}

	private bool HasAvailableChairs()
	{
		return seatHolder.HasAvailableChairs();
	}
}

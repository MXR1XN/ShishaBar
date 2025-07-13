using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
	public class Seat : MonoBehaviour, IAvailablePlace, IInteractible
	{
		[SerializeField] private GameObject interactiveUI;
		[SerializeField] private OpenCanvasOnClick drinkMixingCanvas;

		[SerializeField] private List<TextMeshProUGUI> chairOrderTexts;
		private List<TextMeshProUGUI> kitchenOrderTexts;
		[SerializeField] private List<Button> acceptButtons;
		[SerializeField] private Button closeButton;
		[SerializeField] private Button giveOrdersButton;

		private List<Chair> chairs = new List<Chair>();

		private List<Customer> customers =  new List<Customer>();

		[SerializeField] private GameObject shishaPrefab;
		[SerializeField] private List<Transform> shipSpawnPoints;

		public bool IsAvailable => availablePlaces > 0;
		public Transform SeatPoint => null;
		public Transform Transform => null;
		public Transform LookPoint => null;
		public int ID => id;
		public bool IsUIInitiator { get; set; }

		private int id;
		private static int currentId;

		private int availablePlaces;

		private void Awake() 
		{
			id = currentId++;

			chairs = GetComponentsInChildren<Chair>().ToList();

			var availableChairs = chairs.Where(x => x.IsAvailable).ToList();

			availablePlaces = availableChairs.Count;

			foreach (TextMeshProUGUI text in chairOrderTexts)
			{
				text.text = "";
			}
		}

		private void Start()
		{
			interactiveUI.SetActive(false);
		}
		

		public void UpdateUI() 
		{
			if (interactiveUI == null) 
			{
				return;
			}

			ResetUI();
			AddOrdersToUI();
		}
			

		public void ShowUI()
		{
			interactiveUI.SetActive(true);
			IsUIInitiator = true;
			closeButton.onClick.AddListener(() =>
			{
				foreach (var button in acceptButtons)
				{
					button.onClick.RemoveAllListeners();
				}
			});

			giveOrdersButton.onClick.RemoveAllListeners();
			giveOrdersButton.onClick.AddListener(() =>
			{
				List<Customer> customersToRemove = new();
				Debug.Log("Button clicked - attempting to spawn shisha");
				SpawnShisha();
				foreach (var customer in customers)
				{
					if (Orders.CompleteShishas.Contains(customer.shisha))
					{
						customersToRemove.Add(customer);
					}
				}
				foreach (var customer in customersToRemove)
				{
					customers.Remove(customer);
					Orders.CompleteShishas.Remove(customer.shisha);
					customer.MoveToExit();
					ReleasePlace();
				}
			});
		}
		private void SpawnShisha()
		{
			if (shipSpawnPoints == null)
			{
				Debug.LogError("shipSpawnPoints is not assigned!");
				return;
			}

			foreach (var ship in shipSpawnPoints) 
			{
				var shisha = Instantiate(shishaPrefab, ship.position, Quaternion.identity);
				Destroy(shisha, 5f);
			}
			
		}

		public IAvailablePlace TakePlace(Customer customer)
		{
			if (!IsAvailable)
			{
				return null;
			}
			var avalbileChairs = chairs.Where(c => c.IsAvailable);
			availablePlaces--;
			customer.ShishaOrdered += AttachCustomer;
			customer.ShishaOrdered += customer2 =>
			{
				if (IsUIInitiator) 
				{
					UpdateUI();
				}
			};
			return avalbileChairs.FirstOrDefault().TakePlace(customer);
		}

		private void AttachCustomer(Customer shishaCustomer)
		{
			customers.Add(shishaCustomer);
		}

		public void ResetUI() 
		{
			for (int i = 0; i < chairOrderTexts.Count; i++)
			{
				chairOrderTexts[i].text = "";
			}
		}

		public void AddOrdersToUI() 
		{
			for (int i = 0; i < customers.Count; i++)
			{
				var shisha = customers.ElementAt(i).GetShishaOrder();
				var customer = customers.ElementAt(i);

				if (customer.IsOrderAccepted)
				{
					chairOrderTexts[i].text = "Klient czeka...";
				}
				else
				{
					chairOrderTexts[i].text = $"{shisha.Name}, {shisha.Flavor}, {shisha.Strenght}";
				}

				acceptButtons[i].onClick.AddListener(() =>
				{
					if(!customer.IsOrderAccepted)
					{
						Orders.OrderedShishas.Add(shisha);
						customer.IsOrderAccepted = true;
						UpdateUI();
					}
				});
			}
		}

		public void ReleasePlace()
		{
			availablePlaces++;
			foreach (Chair chair in chairs) 
			{
				chair.ReleasePlace();
			}
			CustomerSpawner.PlaceCount--;
			
		}
	}
}

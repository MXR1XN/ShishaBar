using Assets.Scripts;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KitchenTable : MonoBehaviour, IInteractible
{
	private List<RawImage> contentFields;
	private List<TextMeshProUGUI> kitchenOrderTexts;
	private List<Button> acceptButtons;

	public bool IsUIInitiator { get; set; }

	[SerializeField] private GameObject _content; 
	[SerializeField] private GameObject interactiveUI;


	private void Start()
	{
		interactiveUI.SetActive(false);

		if (_content != null)
		{
			contentFields = _content
				.GetComponentsInChildren<RawImage>().ToList();

			kitchenOrderTexts = contentFields
				.Select(image => image
					.GetComponentInChildren<TextMeshProUGUI>())
						.ToList();

			acceptButtons = contentFields
				.Select(image => image
					.GetComponentInChildren<Button>())
						.ToList();
		}
		else
		{
			throw new MissingReferenceException("The 'Content' field can not be null, text fields can not be assigned.");
		}

	}
	public void ShowUI()
	{
		interactiveUI.SetActive(true);
		IsUIInitiator = true;
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

	public void ResetUI()
	{
		for (int i = 0; i < kitchenOrderTexts.Count; i++)
		{
			kitchenOrderTexts[i].text = "Musisz odebrac zamowienie";
		}
	}

	public void AddOrdersToUI()
	{

		for (int i = 0; i < Orders.OrderedShishas.Count; i++) 
		{
			var shisha = Orders.OrderedShishas.ElementAt(i);
			if(shisha != null)
			{
				string textUI = $"{shisha.Name},{shisha.Flavor},{shisha.Strenght}";

				kitchenOrderTexts[i].text = textUI;
				acceptButtons[i].onClick.AddListener(() => 
				{
					Orders.OrderedShishas.Remove(shisha);
					Orders.CompleteShishas.Add(shisha);
					UpdateUI();
				});
			}
		}
	}

}

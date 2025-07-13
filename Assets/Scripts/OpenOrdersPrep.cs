using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class OpenCanvasOnClick : MonoBehaviour
    {
		[SerializeField] private GameObject drinkMixingCanvas; 
		[SerializeField] private Slider mixingRatioSlider; 
		[SerializeField] private Button decreaseButton; 
		[SerializeField] private Button increaseButton; 
		[SerializeField] private TextMeshProUGUI proportionText;

		public event Action OnClose;

		private float currentSliderValue = 50;
		private const float sensitivity = 0.5f;

		private void Start()
		{
			if (drinkMixingCanvas != null)
			{
				drinkMixingCanvas.SetActive(false);
			}

			if (mixingRatioSlider != null)
			{
				mixingRatioSlider.value = currentSliderValue;
				mixingRatioSlider.interactable = false;
				UpdateProportionText();
			}

			if (decreaseButton != null)
			{
				decreaseButton.onClick.AddListener(DecreaseMixingRatio);
			}

			if (increaseButton != null)
			{
				increaseButton.onClick.AddListener(IncreaseMixingRatio);
			}
		}

		private void Update()
		{
			float tilt = Input.acceleration.x;
			float targetValue = Mathf.Clamp((tilt + 1) * 50, 0, 100);
			currentSliderValue = Mathf.Lerp(currentSliderValue, targetValue, Time.deltaTime * sensitivity);
			mixingRatioSlider.value = currentSliderValue;
			UpdateProportionText();
		}

		public void CloseDrinkMixingCanvas()
		{
			if (drinkMixingCanvas != null)
			{
				drinkMixingCanvas.SetActive(false);
				OnClose?.Invoke();
			}
		}

		public void OpenDrinkMixingCanvas()
		{
			if (drinkMixingCanvas != null)
			{
				drinkMixingCanvas.SetActive(true);
			}
			else
			{
				Debug.LogWarning("DrinkMixingCanvas is not assigned.");
			}
		}

		private void DecreaseMixingRatio()
		{
			if (mixingRatioSlider != null)
			{
				currentSliderValue = Mathf.Clamp(currentSliderValue - 5, 0, 100);
				mixingRatioSlider.value = currentSliderValue;
				UpdateProportionText();
			}
		}

		private void IncreaseMixingRatio()
		{
			if (mixingRatioSlider != null)
			{
				currentSliderValue = Mathf.Clamp(currentSliderValue + 5, 0, 100);
				mixingRatioSlider.value = currentSliderValue;
				UpdateProportionText();
			}
		}

		private void UpdateProportionText()
		{
			if (proportionText != null)
			{
				int value = Mathf.RoundToInt(currentSliderValue);
				proportionText.text = $"{value}% / {100 - value}%";
			}
		}
	}
}

using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Timer : MonoBehaviour
{
	private Text timerTexts;
	private bool timerIsRunning = false;
	public float timeValue = 100;

	private void Start()
	{
		timerTexts = GetComponent<Text>();
		timerIsRunning = true;
	}

	private void Update()
	{
		if (timerIsRunning)
		{
			if (timeValue > 0)
			{
				timeValue -= Time.deltaTime;
			}
			else
			{
				timeValue = 0;
				timerIsRunning = false;
			}

			DisplayTime(timeValue);
		}
	}

	private void DisplayTime(float timeToDisplay) 
	{
		if (timeToDisplay < 0)
		{
			timeToDisplay = 0;
		}

		float minutes = Mathf.FloorToInt(timeToDisplay / 60);
		float seconds = Mathf.FloorToInt(timeToDisplay % 60);
		timerTexts.text = string.Format("{0:00}:{1:00}", minutes, seconds);
	}
}

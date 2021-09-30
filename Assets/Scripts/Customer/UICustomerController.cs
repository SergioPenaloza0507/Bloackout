using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICustomerController : MonoBehaviour
{
	TextMeshProUGUI textCustomerTime;
	[SerializeField] float timeRemainning = 5f;
	[SerializeField] bool timerIsRunning = false;

	private void Awake()
	{
		textCustomerTime = GetComponentInChildren<TextMeshProUGUI>();
	}
	private void Start()
	{
		timerIsRunning = true;
	}
	private void Update()
	{
		if (timerIsRunning)
		{
			if (timeRemainning > 0f)
			{
				timeRemainning -= Time.deltaTime;
				DisplayTime(timeRemainning);
			}
		}


	}
	void DisplayTime(float timeToDisplay)
	{
		float seconds = Mathf.FloorToInt(timeToDisplay % 60);
		textCustomerTime.text = seconds.ToString();
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicBox.EventManagement;
using System;

public class GamePlayController : MonoBehaviour
{
	[SerializeField] private GamePlayView view;

	private static int levelText = 1;

	private int levelProgress = 0;

	private int maxLevelValue=5;

	private void OnEnable()
	{
		EventManager.Instance.AddListener<OnPlayButtonPressedEvent>(OnPlayButtonPressedEventHandler);
		EventManager.Instance.AddListener<OnTostComponentDropsEvent>(OnTostComponentDropsEventHandler);
	}
	private void OnDisable()
	{
		EventManager.Instance.RemoveListener<OnPlayButtonPressedEvent>(OnPlayButtonPressedEventHandler);
		EventManager.Instance.RemoveListener<OnTostComponentDropsEvent>(OnTostComponentDropsEventHandler);
	}


	private void Start()
	{
		//SetGamePlayView();
	}
	private void Update()
	{
		SetGamePlayView();
		if (levelProgress==maxLevelValue)
		{
			levelText++;
			levelProgress = 0;
			maxLevelValue += 2;
		}
	}

	public void OnPauseButtonPresed()
	{
		EventManager.Instance.Raise(new OnExitButtonPressedEvent());
		view.gameObject.SetActive(false);
	}
	private void OnPlayButtonPressedEventHandler(OnPlayButtonPressedEvent eventDetails)
	{
		view.gameObject.SetActive(true);
	}

	private void OnTostComponentDropsEventHandler(OnTostComponentDropsEvent eventDetails)
	{
		levelProgress++;
	}

	private void SetGamePlayView()
	{
		view.SetGamePlayView(levelText.ToString(), levelProgress, maxLevelValue);
	}
}

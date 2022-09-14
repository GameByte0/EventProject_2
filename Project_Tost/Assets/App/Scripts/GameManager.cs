using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicBox.EventManagement;
using System;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject spawner;

	[SerializeField] private GameObject mainCamera;

	private int deadZoneEnterCount;

	private GameState gameState;

	#region Unity Editor

	private void OnEnable()
	{
		EventManager.Instance.AddListener<OnPlayButtonPressedEvent>(OnPlayButtonPressedEventHandler);
		EventManager.Instance.AddListener<OnExitButtonPressedEvent>(OnExitButtonPressedEventHandler);
		EventManager.Instance.AddListener<OnDeadZoneEnterEvent>(OnDeadZoneEnterEventHandler);
	}

	private void OnDisable()
	{
		EventManager.Instance.RemoveListener<OnPlayButtonPressedEvent>(OnPlayButtonPressedEventHandler);
		EventManager.Instance.RemoveListener<OnExitButtonPressedEvent>(OnExitButtonPressedEventHandler);
		EventManager.Instance.AddListener<OnDeadZoneEnterEvent>(OnDeadZoneEnterEventHandler);
	}

	private void Update()
	{
		if (deadZoneEnterCount==3)
		{
			gameState = GameState.GameOver;
		}
		if (gameState == GameState.GamePlay)
		{
			spawner.SetActive(true);
			mainCamera.transform.SetParent(spawner.transform);
		}
		else if (gameState == GameState.Pause)
		{
			spawner.SetActive(false);
			mainCamera.transform.SetParent(null);
		}
		else if (gameState == GameState.GameOver)
		{
			deadZoneEnterCount = 0;
		}
	}
	#endregion

	private void OnPlayButtonPressedEventHandler(OnPlayButtonPressedEvent eventDetails)
	{
		gameState = GameState.GamePlay;
	}
	private void OnExitButtonPressedEventHandler(OnExitButtonPressedEvent eventDetails)
	{
		gameState = GameState.Pause;
	}
	private void OnDeadZoneEnterEventHandler(OnDeadZoneEnterEvent eventDetails)
	{
		deadZoneEnterCount++;
		print(deadZoneEnterCount);
	}
	private enum GameState
	{
		MainMenu,
		GamePlay,
		Pause,
		GameOver
	}
}

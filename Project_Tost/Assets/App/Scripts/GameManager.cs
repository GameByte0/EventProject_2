using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicBox.EventManagement;
using System;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject spawner;

	[SerializeField] private GameObject mainCamera;

	private GameState gameState;

	#region Unity Editor

	private void OnEnable()
	{
		EventManager.Instance.AddListener<OnPlayButtonPressedEvent>(OnPlayButtonPressedEventHandler);
		EventManager.Instance.AddListener<OnExitButtonPressedEvent>(OnExitButtonPressedEventHandler);
	}

	private void OnDisable()
	{
		EventManager.Instance.RemoveListener<OnPlayButtonPressedEvent>(OnPlayButtonPressedEventHandler);
		EventManager.Instance.RemoveListener<OnExitButtonPressedEvent>(OnExitButtonPressedEventHandler);
	}

	private void Update()
	{
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

	private enum GameState
	{
		MainMenu,
		GamePlay,
		Pause
	}
}

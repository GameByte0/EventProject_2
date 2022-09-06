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
	}

	private void OnDisable()
	{
		EventManager.Instance.RemoveListener<OnPlayButtonPressedEvent>(OnPlayButtonPressedEventHandler);
	}

	private void Update()
	{
		if (gameState==GameState.GamePlay)
		{
			spawner.SetActive(true);
			mainCamera.transform.SetParent(spawner.transform);
		}
	}
	#endregion

	private void OnPlayButtonPressedEventHandler(OnPlayButtonPressedEvent eventDetails)
	{
		gameState = GameState.GamePlay;
	}

	private enum GameState
	{
		MainMenu,
		GamePlay,
		Pause
	}
}

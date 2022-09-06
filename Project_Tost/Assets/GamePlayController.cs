using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicBox.EventManagement;
using System;

public class GamePlayController : MonoBehaviour
{
	[SerializeField] private GamePlayView view;
	private void OnEnable()
	{
		EventManager.Instance.AddListener<OnPlayButtonPressedEvent>(OnPlayButtonPressedEventHandler);
	}
	private void OnDisable()
	{
		EventManager.Instance.RemoveListener<OnPlayButtonPressedEvent>(OnPlayButtonPressedEventHandler);
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
}

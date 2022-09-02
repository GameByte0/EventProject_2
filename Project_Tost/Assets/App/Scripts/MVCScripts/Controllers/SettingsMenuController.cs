using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicBox.EventManagement;
using System;

public class SettingsMenuController : MonoBehaviour
{
  [SerializeField] private SettingsMenuView view;

	private void OnEnable()
	{
		EventManager.Instance.AddListener<OnSettingsMenuButtonPressedEvent>(OnSettingsMenuButtonPressedEventHandler);
	}

	private void OnDisable()
	{
		EventManager.Instance.RemoveListener<OnSettingsMenuButtonPressedEvent>(OnSettingsMenuButtonPressedEventHandler);
	}

	private void OnSettingsMenuButtonPressedEventHandler(OnSettingsMenuButtonPressedEvent eventDetails)
	{
		view.gameObject.SetActive(true);
	}


	public void OnExitButtonPressed()
	{
		EventManager.Instance.Raise(new OnExitButtonPressedEvent());
		view.gameObject.SetActive(false);
	}
}

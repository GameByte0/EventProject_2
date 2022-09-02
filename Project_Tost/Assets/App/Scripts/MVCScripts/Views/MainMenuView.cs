using DynamicBox.EventManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
	[SerializeField] private MainMenuController controller;

	[SerializeField] private Button startButton;

	[SerializeField] private Button shopButton;

	[SerializeField] private Button settingsButton;


	public void OnShopMenuButtonPressed()
	{
		controller.OnShopMenuButtonPressed();
	}
	public void OnSettingsMenuButtonPressed()
	{
		controller.OnSettingsMenuButtonPressed();
	}
}

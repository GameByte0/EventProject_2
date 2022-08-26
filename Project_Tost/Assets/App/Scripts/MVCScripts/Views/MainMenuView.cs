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

	[SerializeField] private GameObject mainMenuPanel;

	[SerializeField] private GameObject shopMenuPanel;

	[SerializeField] private GameObject settingsMenuPanel;


	public void OnPlayButtonPressed()
	{
		shopMenuPanel.SetActive(false);

		settingsMenuPanel.SetActive(false);

		mainMenuPanel.SetActive(false);
	}

	public void OnSettingsButtonPressed()
	{
		shopMenuPanel.SetActive(false);

		settingsMenuPanel.SetActive(true);

		mainMenuPanel.SetActive(false);
	}


	public void OnShopButtonPressed()
	{
		shopMenuPanel.SetActive(true);

		settingsMenuPanel.SetActive(false);

		mainMenuPanel.SetActive(false);
	}

	public void OnExitButtonPressed()
	{
		shopMenuPanel.SetActive(false);

		settingsMenuPanel.SetActive(false);

		mainMenuPanel.SetActive(true);
	}
}

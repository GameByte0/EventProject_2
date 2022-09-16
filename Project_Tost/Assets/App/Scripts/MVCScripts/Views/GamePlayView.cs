using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DynamicBox.EventManagement;

public class GamePlayView : MonoBehaviour
{
	[SerializeField] private GamePlayController controller;

	[Header("GamePlayUI References:")]
	[SerializeField] private Slider _progressSlider;

	[SerializeField] private TMP_Text _levelText;

	[SerializeField] private GameObject GameOver;

	[Header("HealthUI Components:")]
	[SerializeField] private GameObject healthPanle;

	[SerializeField] private GameObject healthImage;

	[SerializeField] private List<GameObject> healthImageList;

	public void OnPauseButtonPresed()
	{
		controller.OnPauseButtonPresed();
	}

	public void SetGamePlayView(string levelText, int levelProgress, int maxLevelValue)
	{
		_levelText.text = levelText;

		_progressSlider.value = levelProgress;

		_progressSlider.maxValue = maxLevelValue;

	}

	public void SetHealth(int health)
	{
		if (healthImageList.Count==health)
		{
			return;
		}
		for (int i = 0; i < health; i++)
		{
			GameObject healthIconInstance = Instantiate(healthImage, healthPanle.transform);
			healthImageList.Add(healthIconInstance);
		}
	}

	public void ChangeHealthCount()
	{
		if (healthImageList.Count!=0)
		{
			Destroy(healthImageList[healthImageList.Count-1]);
			healthImageList.Remove(healthImageList[healthImageList.Count-1]);
		}
		else
		{
			GameOver.SetActive(true);
		}
		
	}
	public void RestartScene()
	{
		controller.ResetScene();
		GameOver.SetActive(false);
	}

}


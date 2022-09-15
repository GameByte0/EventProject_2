using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayView : MonoBehaviour
{
	[SerializeField] private GamePlayController controller;

	[Header("GamePlayUI References:")]
	[SerializeField] private Slider _progressSlider;

	[SerializeField] private TMP_Text _levelText;

	[SerializeField] private GameObject GameOver;

	[Header("HealthUI Components:")]
	[SerializeField] private GameObject healthPanle;

	[SerializeField] private Image healthImage;

	[SerializeField] private List<Image> healthImageList;

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
		for (int i = 0; i < health; i++)
		{
			Image healthIconInstance = Instantiate(healthImage, healthPanle.transform);
			healthImageList.Add(healthIconInstance);
		}
	}

	public void ChangeHealthCount()
	{
		if (healthImageList.Count!=0)
		{
			Destroy(healthImageList[0]);
			healthImageList.Remove(healthImageList[0]);
		}
		else
		{
			GameOver.SetActive(true);
		}
		
	}

}


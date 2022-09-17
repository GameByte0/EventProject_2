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

	[SerializeField] private GameObject startPanel;

	[Header("HealthUI Components:")]
	[SerializeField] private GameObject healthPanle;

	[SerializeField] private GameObject healthImage;

	[SerializeField] private List<GameObject> healthImageList;

	[Header("PauseUI References:")]
	[SerializeField] private GameObject pausePanel;

	[Header("GameOverUI References:")]
	[SerializeField] private GameObject GameOver;

	[SerializeField] private TMP_Text highScore;

	[SerializeField] private TMP_Text currentScore;

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			startPanel.SetActive(false);
		}
	}
	public void OnPauseButtonPresed()
	{
		pausePanel.SetActive(true);
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
			controller.SetGameOverData();
		}
		
	}
	public void RestartScene()
	{
		controller.ResetScene();
		GameOver.SetActive(false);
	}
	public void ResumeButtonPressed()
	{
		pausePanel.SetActive(false);
	}
	public void MenuButtonPressed()
	{
		controller.MenuButtonPressed();
		pausePanel.SetActive(false);
	}
	public void SetGameOverData(int HighScore,int CurrentScore)
	{
		highScore.text =HighScore.ToString();
		currentScore.text =CurrentScore.ToString();
	}

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayView : MonoBehaviour
{
  [SerializeField] private GamePlayController controller;

	[SerializeField] private Slider _progressSlider;

	[SerializeField] private TMP_Text _levelText;

  public void OnPauseButtonPresed()
	{
		controller.OnPauseButtonPresed();
	}

	public void SetGamePlayView(string levelText, int levelProgress,int maxLevelValue )
	{
		_levelText.text = levelText;

		_progressSlider.value = levelProgress;

		_progressSlider.maxValue = maxLevelValue;

		
	}
}

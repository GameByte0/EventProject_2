using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GamePlayView : MonoBehaviour
{
  [SerializeField] private GamePlayController controller;

	[SerializeField] private Slider progressSlider;

	[SerializeField] private TMP_Text levelText;

  public void OnPauseButtonPresed()
	{
		controller.OnPauseButtonPresed();
	}
}

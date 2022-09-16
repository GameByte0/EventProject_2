using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicBox.EventManagement;
using UnityEngine.SceneManagement;

public class GamePlayController : MonoBehaviour
{
	[SerializeField] private GamePlayView view;


	private int maxHealth=3;

	private static int levelText = 1;

	private int levelProgress = 0;

	private int maxLevelValue=5;

	private void OnEnable()
	{
		EventManager.Instance.AddListener<OnPlayButtonPressedEvent>(OnPlayButtonPressedEventHandler);
		EventManager.Instance.AddListener<OnTostComponentDropsEvent>(OnTostComponentDropsEventHandler);
		EventManager.Instance.AddListener<OnDeadZoneEnterEvent>(OnDeadZoneEnterEventHandler);
		EventManager.Instance.AddListener<OnRestartButtonPressedEvent>(OnRestartButtonPressedEventHandler);
	}
	private void OnDisable()
	{
		EventManager.Instance.RemoveListener<OnPlayButtonPressedEvent>(OnPlayButtonPressedEventHandler);
		EventManager.Instance.RemoveListener<OnTostComponentDropsEvent>(OnTostComponentDropsEventHandler);
		EventManager.Instance.RemoveListener<OnDeadZoneEnterEvent>(OnDeadZoneEnterEventHandler);
		EventManager.Instance.RemoveListener<OnRestartButtonPressedEvent>(OnRestartButtonPressedEventHandler);
	}

	

	private void Start()
	{
		
	}
	private void Update()
	{
		SetGamePlayView();
		if (levelProgress==maxLevelValue)
		{
			levelText++;
			levelProgress = 0;
			maxLevelValue += 2;
		}
		if (levelProgress<0)
		{
			levelProgress = 0;
		}
	}

	public void OnPauseButtonPresed()
	{
		EventManager.Instance.Raise(new OnExitButtonPressedEvent());
		view.gameObject.SetActive(false);
	}
	private void SetGamePlayView()
	{
		view.SetGamePlayView(levelText.ToString(), levelProgress, maxLevelValue);
	}


	private void SetHealth()
	{
		view.SetHealth(maxHealth);
	}
	private void ChangeHealthCount()
	{
		view.ChangeHealthCount();
	}
	public void ResetScene()
	{
		EventManager.Instance.Raise(new OnRestartButtonPressedEvent());
	}



	#region EventHandlers
	private void OnPlayButtonPressedEventHandler(OnPlayButtonPressedEvent eventDetails)
	{
		view.gameObject.SetActive(true);
		SetHealth();
	}

	private void OnTostComponentDropsEventHandler(OnTostComponentDropsEvent eventDetails)
	{
		levelProgress++;
		
	}
	private void OnDeadZoneEnterEventHandler(OnDeadZoneEnterEvent eventDetails)
	{
		levelProgress--;
		
		ChangeHealthCount();
	}
	private void OnRestartButtonPressedEventHandler(OnRestartButtonPressedEvent eventDetails)
	{
		SetHealth();
	}
	#endregion

}

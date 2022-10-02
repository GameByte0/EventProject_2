using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicBox.EventManagement;
using UnityEngine.SceneManagement;

public class GamePlayController : MonoBehaviour
{
	[SerializeField] private GamePlayView view;


	private int maxHealth = 3;

	private int levelText = 1;

	private int levelProgress = 0;

	private int maxLevelValue = 5;

	private int highScore=1;

	//private int levelIndex=0;

	private bool isLimitReached=false;

	private bool isContinuing=false;

	private bool isLocationChanged=true;

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
		highScore = PlayerPrefs.GetInt("HighScore");
		
	}
	private void Update()
	{
		SetGamePlayView();
		if (levelProgress == maxLevelValue)
		{
			if (levelText<5)
			{
				maxLevelValue += 2;
			}
			levelText++;
			levelProgress = 0;
			
			isLimitReached = false;
		}
		if (levelProgress < 0)
		{
			levelProgress = 0;
		}
		if (levelText%5==0)
		{
			NextLevel();
		}
	}

	public void MenuButtonPressed()
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
	private void NextLevel()
	{

		if (isLimitReached==false)
		{
			isContinuing = true;
			isLimitReached = true;

			//PlayerPrefs.SetInt()
			if (isLocationChanged)
			{
				EventManager.Instance.Raise(new OnLocationChangedEvent(1));
				isLocationChanged = !isLocationChanged;
			}
			else
			{
				EventManager.Instance.Raise(new OnLocationChangedEvent(0));
				isLocationChanged = !isLocationChanged;
			}
			
			EventManager.Instance.Raise(new OnLevelChangedEvent(1));

			EventManager.Instance.Raise(new OnRestartButtonPressedEvent());

		}
		
	}
	public void ResetScene()
	{
		EventManager.Instance.Raise(new OnRestartButtonPressedEvent());
		levelProgress = 0;
		maxLevelValue = 5;
		levelText = 1;
		EventManager.Instance.Raise(new OnLevelChangedEvent(0));
	}
	public void MenuButtonPresse()
	{
		//view.//pause off;//
		//EventManager.Instance.Raise(new OnExitButtonPressedEvent());
	}
	public void GameOver()
	{
		isContinuing = false;
	}
	public void SetGameOverData()
	{
		view.SetGameOverData(HighScoreCounter(), levelText);
	}
	private int HighScoreCounter()
	{ 
		if (highScore <= levelText)
		{
			highScore = levelText;
			PlayerPrefs.SetInt("HighScore", highScore);
			return PlayerPrefs.GetInt("HighScore");
		}
		else
		{
			return PlayerPrefs.GetInt("HighScore");
		}
		
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
		if (!isContinuing)
		{
			SetHealth();
		}
		
	}
	#endregion

}

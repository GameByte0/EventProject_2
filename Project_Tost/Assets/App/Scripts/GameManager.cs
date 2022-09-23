using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicBox.EventManagement;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] private GameObject spawner;

	[SerializeField] private GameObject mainCamera;

	private static bool isReloaded=false;

	private GameState gameState;

	#region Unity Editor

	private void Awake()
	{
		Time.timeScale = 1;
		if (isReloaded)
		{
			gameState = GameState.GamePlay;
		}
	}
	private void OnEnable()
	{
		EventManager.Instance.AddListener<OnPlayButtonPressedEvent>(OnPlayButtonPressedEventHandler);
		EventManager.Instance.AddListener<OnExitButtonPressedEvent>(OnExitButtonPressedEventHandler);
		EventManager.Instance.AddListener<OnDeadZoneEnterEvent>(OnDeadZoneEnterEventHandler);
		EventManager.Instance.AddListener<OnRestartButtonPressedEvent>(OnRestartButtonPressedEventHandler);
		EventManager.Instance.AddListener<OnLocationChangedEvent>(OnLocationChangedEventHandler);
	}

	private void OnDisable()
	{
		EventManager.Instance.RemoveListener<OnPlayButtonPressedEvent>(OnPlayButtonPressedEventHandler);
		EventManager.Instance.RemoveListener<OnExitButtonPressedEvent>(OnExitButtonPressedEventHandler);
		EventManager.Instance.RemoveListener<OnDeadZoneEnterEvent>(OnDeadZoneEnterEventHandler);
		EventManager.Instance.RemoveListener<OnRestartButtonPressedEvent>(OnRestartButtonPressedEventHandler);
		EventManager.Instance.RemoveListener<OnLocationChangedEvent>(OnLocationChangedEventHandler);
	}

	private void Update()
	{
		if (gameState == GameState.GamePlay)
		{
			spawner.SetActive(true);
			mainCamera.transform.SetParent(spawner.transform);
		}
		else if (gameState == GameState.Pause)
		{
			spawner.SetActive(false);
			mainCamera.transform.SetParent(null);
		}
		else if (gameState == GameState.GameOver)
		{
			Time.timeScale = 0.2f;
		}
	}
	#endregion

	private void OnPlayButtonPressedEventHandler(OnPlayButtonPressedEvent eventDetails)
	{
		Time.timeScale = 1f;
		gameState = GameState.GamePlay;
	}
	private void OnExitButtonPressedEventHandler(OnExitButtonPressedEvent eventDetails)
	{
		gameState = GameState.Pause;
		//Time.timeScale =0;
	}
	private void OnDeadZoneEnterEventHandler(OnDeadZoneEnterEvent eventDetails)
	{
		
	}
	private void OnRestartButtonPressedEventHandler(OnRestartButtonPressedEvent eventDetails)
	{
		SceneManager.UnloadSceneAsync(1);
		SceneManager.LoadSceneAsync(1,LoadSceneMode.Additive);
		isReloaded = true;
		Time.timeScale = 1f;
	}
	private void OnLocationChangedEventHandler(OnLocationChangedEvent eventDetails)
	{
		StartCoroutine(ReloadAsync());
	}
	private IEnumerator ReloadAsync()
	{
		yield return new WaitForSeconds(0.2f);
		SceneManager.UnloadSceneAsync(1);
		SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
	}
	private enum GameState
	{
		MainMenu,
		GamePlay,
		Pause,
		GameOver
	}
}

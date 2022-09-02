using DynamicBox.EventManagement;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
	[SerializeField] private MainMenuView view;

	private void OnEnable()
	{
		EventManager.Instance.AddListener<OnExitButtonPressedEvent>(OnExitButtonPressedEventHandler);
	}
	private void OnDisable()
	{
		EventManager.Instance.RemoveListener<OnExitButtonPressedEvent>(OnExitButtonPressedEventHandler);
	}
	public void OnShopMenuButtonPressed()
	{
		EventManager.Instance.Raise(new OnShopMenuButtonsPressedEvent());
		view.gameObject.SetActive(false);
	}
	public void OnSettingsMenuButtonPressed()
	{
		EventManager.Instance.Raise(new OnSettingsMenuButtonPressedEvent());
		view.gameObject.SetActive(false);
	}

	private void OnExitButtonPressedEventHandler(OnExitButtonPressedEvent eventDetails)
	{
		view.gameObject.SetActive(true);
	}
}

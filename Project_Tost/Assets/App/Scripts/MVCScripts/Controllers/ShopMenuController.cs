using DynamicBox.EventManagement;
using UnityEngine;

public class ShopMenuController : MonoBehaviour
{
	[SerializeField] ShopMenuView view;
	private void OnEnable()
	{
		EventManager.Instance.AddListener<OnShopMenuButtonsPressedEvent>(OnShopMenuButtonsPressedEventHandler);
	}

	private void OnDisable()
	{
		EventManager.Instance.RemoveListener<OnShopMenuButtonsPressedEvent>(OnShopMenuButtonsPressedEventHandler);
	}

	private void OnShopMenuButtonsPressedEventHandler(OnShopMenuButtonsPressedEvent eventDetails)
	{
		view.gameObject.SetActive(true);
	}

	public void OnExitButtonPressed()
	{
		EventManager.Instance.Raise(new OnExitButtonPressedEvent());
		view.gameObject.SetActive(false);
	}
}
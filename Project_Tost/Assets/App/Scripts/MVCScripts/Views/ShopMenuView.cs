using UnityEngine;

public class ShopMenuView : MonoBehaviour
{
	[SerializeField] private ShopMenuController controller;

	public void OnExitButtonPressed()
	{
		controller.OnExitButtonPressed();
	}
}

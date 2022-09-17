using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopMenuView : MonoBehaviour
{
	[SerializeField] private TMP_Text highScore;

	[SerializeField] private ShopMenuController controller;

	[SerializeField] private Button picnicButton;

	[SerializeField] private Button woodlandiaButton;

	public void OnExitButtonPressed()
	{
		controller.OnExitButtonPressed();
	}
	public void SetHighScore(int HighScore)
	{
		highScore.text ="High Score: "+ HighScore;
	}
	public void SelectPicnicLocation()
	{
		controller.SelectPicnicLocation();
		picnicButton.interactable = false;
		woodlandiaButton.interactable = true;
	}
	public void SelectWoodlandiaLocation()
	{
		controller.SelectWoodlandiaLocation();
		woodlandiaButton.interactable = false;
		picnicButton.interactable = true;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuView : MonoBehaviour
{
  [SerializeField] private SettingsMenuController controller;

  public void OnExitButtonPressed()
	{
    controller.OnExitButtonPressed();
	}
   
}

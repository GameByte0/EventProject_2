using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicBox.EventManagement;

public class OnDeadZoneEnterEvent : GameEvent
{
  public float LoweringHeight;
	public OnDeadZoneEnterEvent(float loweringHeight)
	{
		LoweringHeight = loweringHeight;
	}
}

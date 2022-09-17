using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicBox.EventManagement;

public class OnLocationChangedEvent : GameEvent
{
  public int locationID;

	public OnLocationChangedEvent(int locationID)
	{
		this.locationID = locationID;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicBox.EventManagement;

public class OnLevelChangedEvent : GameEvent
{
  public float SpawnerSpeed;

	public OnLevelChangedEvent(float _SpawenerSpeed)
	{
		SpawnerSpeed = _SpawenerSpeed;
	}
}

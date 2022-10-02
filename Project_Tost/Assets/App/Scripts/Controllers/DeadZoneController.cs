using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicBox.EventManagement;

public class DeadZoneController : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (!other.GetComponent<TostComponentController>().isDeadZoneEntered && other.GetComponent<Rigidbody>().isKinematic==false)
		{
			EventManager.Instance.Raise(new OnDeadZoneEnterEvent(other.gameObject.transform.localScale.y));
			other.GetComponent<TostComponentController>().isDeadZoneEntered = true;
		}
		
	}
}

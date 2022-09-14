using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicBox.EventManagement;

public class TostComponentController : MonoBehaviour
{
	public bool isDeadZoneEntered=false;
	private void OnCollisionEnter(Collision collision)
	{
		//gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
	}
}

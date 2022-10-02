using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicBox.EventManagement;

public class TostComponentController : MonoBehaviour
{
	public bool isDeadZoneEntered=false;
	private void OnCollisionEnter(Collision collision)
	{
		gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0,0,0);
	}
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
	[SerializeField] private GameObject[] tostComponents;

	[SerializeField] private GameObject mainCamera;

	[SerializeField] private bool componentState;



	private int currentComponent;
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			currentComponent = Random.Range(0, tostComponents.Length);

			GameObject tostComponentInstance = Instantiate(tostComponents[currentComponent], transform.position, transform.rotation);

			tostComponentInstance.GetComponent<Rigidbody>().isKinematic = false;

			mainCamera.transform.position += new Vector3(0f, tostComponentInstance.transform.localScale.y, 0f);

			transform.position = new Vector3(0f, mainCamera.transform.position.y, 0f);
		}
	}
}

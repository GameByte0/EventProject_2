using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SpawnerController : MonoBehaviour
{
	[SerializeField] private List<GameObject> pooledObjects;

	[SerializeField] private GameObject[] tostComponents;

	[SerializeField] private int poolAmount;


	GameObject tostComponentInstance;

	#region UnityEditor
	private void Start()
	{
		pooledObjects = new List<GameObject>();
		GameObject tostComponentInstance;

		foreach (GameObject component in tostComponents)
		{
			for (int i = 0; i < poolAmount; i++)
			{
				tostComponentInstance = Instantiate(component,transform);
				tostComponentInstance.transform.SetParent(transform);
				tostComponentInstance.SetActive(false);
				pooledObjects.Add(tostComponentInstance);
			}
		}

	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			pooledObjects.Remove(tostComponentInstance);
			tostComponentInstance.SetActive(true);
		}
	}
	#endregion

}

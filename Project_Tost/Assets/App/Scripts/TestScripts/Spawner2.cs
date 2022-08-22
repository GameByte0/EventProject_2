using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner2 : MonoBehaviour
{
	[SerializeField] private Dictionary<string, List<GameObject>> componentsDictionary;

	[SerializeField] private List<LocationsSO> locationList;

	[SerializeField] private List<GameObject> mainPool;

	[SerializeField] private int poolSize;

	[SerializeField] private Location currentLocation;

	private GameObject currentlyPoolingObject;



	#region Unity Methods
	private void Awake()
	{
		componentsDictionary = new Dictionary<string, List<GameObject>>();

		//locationList = new List<LocationsSO>();

		mainPool = new List<GameObject>();

	}

	private void Start()
	{
		for (int i = 0; i < locationList.Count; i++)
		{
			componentsDictionary.Add(locationList[i].LocationName, locationList[i].LocationsTostComponents);
		}

		PoolingProcess();
	}
	private void Update()
	{
		

		if (Input.GetKeyDown(KeyCode.Space))
		{
			currentlyPoolingObject = mainPool[Random.Range(0, mainPool.Count)];

			if (!currentlyPoolingObject.activeInHierarchy && mainPool.Count != 0)
			{
				currentlyPoolingObject.transform.position = transform.position;

				currentlyPoolingObject.SetActive(true);

				mainPool.Remove(currentlyPoolingObject);

			}
		}
	}
	#endregion

	private void PoolingProcess()
	{
		switch (currentLocation)
		{
			case Location.DEFAULT:
				mainPool.Clear();
				foreach (GameObject obj in componentsDictionary["Default"])
				{
					for (int i = 0; i < poolSize; i++)
					{
						GameObject objInstance = Instantiate(obj);
						objInstance.SetActive(false);

						mainPool.Add(obj);
					}
				}
				break;
			case Location.WOODLANDIA:
				mainPool.Clear();
				foreach (GameObject obj in componentsDictionary["Woodlandia"])
				{
					for (int i = 0; i < poolSize; i++)
					{
						GameObject objInstance = Instantiate(obj);
						objInstance.SetActive(false);

						mainPool.Add(obj);
					}
				}
				break;
			default:
				Debug.Log("Location is not Selected ");
				break;
		}
	}
	private enum Location
	{
		DEFAULT,
		WOODLANDIA
	}
}

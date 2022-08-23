using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController2 : MonoBehaviour
{
	[SerializeField] private Dictionary<string, List<GameObject>> componentsDictionary;

	[SerializeField] private List<LocationsSO> locationList;

	[SerializeField] private List<GameObject> mainPool;

	[SerializeField] private int poolSize;

	[SerializeField] private Location currentLocation;

	[SerializeField] private int speed;

	private GameObject currentlyPoolingObject;



	#region Unity Methods
	private void Awake()
	{
		componentsDictionary = new Dictionary<string, List<GameObject>>();

		//If some object added via Unity Editor you dont need to write this
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
		SpawnerMove();

		if (Input.GetKeyDown(KeyCode.Space))
		{
			currentlyPoolingObject = mainPool[Random.Range(0, mainPool.Count)];

			if (!currentlyPoolingObject.activeInHierarchy)
			{
				currentlyPoolingObject.SetActive(true);
				currentlyPoolingObject.transform.position = transform.position;
				StartCoroutine(MakeDynamic(currentlyPoolingObject));
				SpawnerPositionChange(currentlyPoolingObject.transform.localScale.y);
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

						mainPool.Add(objInstance);
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

						mainPool.Add(objInstance);
					}
				}
				break;
			default:
				Debug.Log("Location is not Selected ");
				break;
		}
	}

	private IEnumerator MakeDynamic(GameObject obj)
	{
		yield return new WaitForSeconds(0.2f);
		obj.GetComponent<Rigidbody>().isKinematic = false;
	}

	private void SpawnerPositionChange(float height)
	{
		//Raises Spawner's positioan by spawned objects localScale.y
		transform.position += new Vector3(0, height, 0);
	}

	private void SpawnerMove()
	{
		//Makes Spawner move horizontally
		transform.position = new Vector3(Mathf.PingPong(Time.time * speed, 10) - 5, transform.position.y, transform.position.z);
	}
	private enum Location
	{
		DEFAULT,
		WOODLANDIA
	}
}

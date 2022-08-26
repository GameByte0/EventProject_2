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

	private GameObject currentlyUsingObject;

	private bool isActivated=true;



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
		MakeVisible();

		if (Input.GetKeyDown(KeyCode.Space))
		{
			
			currentlyUsingObject.transform.SetParent(null);
			StartCoroutine(MakeDynamic(currentlyUsingObject));
			SpawnerPositionChange(currentlyUsingObject.transform.localScale.y);
			mainPool.Remove(currentlyUsingObject);

			isActivated = true;
		}
	}
	#endregion

	private void PoolingProcess()
	{//if need to add new location just create scriptable object and case for location//
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
						objInstance.transform.SetParent(transform);

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
						objInstance.transform.SetParent(transform);

						mainPool.Add(objInstance);
					}
				}
				break;

			case Location.MIX:
				mainPool.Clear();
				foreach (GameObject obj in componentsDictionary["Mix"])
				{
					for (int i = 0; i < poolSize; i++)
					{
						GameObject objInstance = Instantiate(obj);
						objInstance.SetActive(false);
						objInstance.transform.SetParent(transform);

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
		yield return new WaitForEndOfFrame();
		obj.GetComponent<Rigidbody>().isKinematic = false;
	}

	private void SpawnerPositionChange(float height)
	{
		//Raises Spawner's positioan by spawned objects localScale.y
		transform.position += new Vector3(0, height+0.5f, 0);

		//Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit);
		//transform.position += new Vector3(0,hit.transform.position.y+5.5f,0);
	}

	private void SpawnerMove()
	{
		//Makes Spawner move horizontally
		transform.position = new Vector3(Mathf.PingPong(Time.time * speed, 10) - 5, transform.position.y, transform.position.z);
	}

	private void MakeVisible()
	{
		if (isActivated)
		{
			currentlyUsingObject = mainPool[Random.Range(0, mainPool.Count)];
			currentlyUsingObject.SetActive(true);
			currentlyUsingObject.transform.position = transform.position;
			isActivated = false;
		}
		

	}

	private enum Location
	{
		DEFAULT,
		WOODLANDIA,
		MIX
	}
}

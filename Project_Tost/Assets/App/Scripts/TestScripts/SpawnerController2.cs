using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicBox.EventManagement;

public class SpawnerController2 : MonoBehaviour
{

	[SerializeField] private Dictionary<string, List<GameObject>> componentsDictionary;

	[Header("Pools and Settings")]
	[SerializeField] private List<LocationsSO> locationList;

	[SerializeField] private List<GameObject> mainPool;

	[SerializeField] private List<GameObject> reservPool;

	[SerializeField] private int poolSize;

	[Header("Additional Settings")]
	[SerializeField] GameObject DeadZone;

	[SerializeField] private Location currentLocation;

	[SerializeField] private int speed;

	private GameObject currentlyUsingObject;

	private Vector3 defaultSpawnerPosition = new Vector3(0f, 3f, 0f);

	private bool isActivated = true;

	private float cooldownTime = 1f;

	private float nextDropTime;

	private int reservPoolIndex;

	private bool isReserveObjectAdded;



	#region Unity Methods
	private void OnEnable()
	{
		EventManager.Instance.AddListener<OnTostComponentCollidesEvent>(OnTostComponentCollidesEventHandler);
		EventManager.Instance.AddListener<OnDeadZoneEnterEvent>(OnDeadZoneEnterEventHandler);
	}
	private void OnDisable()
	{
		EventManager.Instance.AddListener<OnTostComponentCollidesEvent>(OnTostComponentCollidesEventHandler);
		EventManager.Instance.RemoveListener<OnDeadZoneEnterEvent>(OnDeadZoneEnterEventHandler);

	}
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

		DeactivateTostComponent();

		if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextDropTime)
		{
			//CooldDown process
			nextDropTime = Time.time + cooldownTime;

			currentlyUsingObject.transform.SetParent(null);
			StartCoroutine(MakeDynamic(currentlyUsingObject));
			SpawnerPositionChange(currentlyUsingObject.transform.localScale.y,"Up");
			mainPool.Remove(currentlyUsingObject);
			reservPool.Add(currentlyUsingObject);
			isReserveObjectAdded = true;
			//currentlyUsingObject.GetComponent<Rigidbody>().velocity = Vector3.down * 10;
			EventManager.Instance.Raise(new OnTostComponentDropsEvent());

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
		yield return new WaitForFixedUpdate();
		obj.GetComponent<Rigidbody>().isKinematic = false;
	}

	private void SpawnerPositionChange(float height,string direction)
	{
		//Raises Spawner's positioan by spawned objects localScale.y//

		


		if (direction=="Up")
		{
			//***Remove 0.5f after adding real prefabs***//
			transform.position += new Vector3(0, height + 0.5f, 0);
			DeadZone.transform.position += new Vector3(0f, height, 0f);
		}
		else if (direction=="Down")
		{
			transform.position -= new Vector3(0, height + 0.5f, 0);
			DeadZone.transform.position -= new Vector3(0f, height, 0f);
		}
	}

	private void SpawnerMove()
	{
		//Makes Spawner move horizontally
		transform.position = new Vector3(Mathf.PingPong(Time.time * speed, 10) - 5, transform.position.y, transform.position.z);
	}

	private void MakeVisible()
	{
		if (isActivated && mainPool.Count != 0)
		{
			currentlyUsingObject = mainPool[Random.Range(0, mainPool.Count)];
			currentlyUsingObject.SetActive(true);
			currentlyUsingObject.transform.position = transform.position;
			isActivated = false;
		}


	}

	private void DeactivateTostComponent()
	{
		if (reservPool.Count >= 10 && isReserveObjectAdded)
		{
			GameObject componentToDeactivate = reservPool[reservPoolIndex];
			componentToDeactivate.GetComponent<Rigidbody>().isKinematic = true;
			reservPoolIndex++;
			isReserveObjectAdded = false;
			//Work with pool clearing integration
		}
	}
	private void OnTostComponentCollidesEventHandler(OnTostComponentCollidesEvent eventDetails)
	{
		DeactivateTostComponent();
	}
	private void OnDeadZoneEnterEventHandler(OnDeadZoneEnterEvent eventDetails)
	{
		SpawnerPositionChange(eventDetails.LoweringHeight, "Down");
	}
	private enum Location
	{
		DEFAULT,
		WOODLANDIA,
		MIX
	}
}

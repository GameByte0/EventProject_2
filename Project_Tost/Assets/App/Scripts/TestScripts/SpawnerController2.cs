using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicBox.EventManagement;

public class SpawnerController2 : MonoBehaviour
{

	[SerializeField] private Dictionary<string, List<GameObject>> componentsDictionary;
	[SerializeField] private int LocationID;
	[Header("Pools and Settings")]
	[SerializeField] private List<LocationsSO> locationList;

	[SerializeField] private List<GameObject> mainPool;

	[SerializeField] private List<GameObject> reservPool;

	[SerializeField] private int poolSize;

	private int poolIndex;

	private int pooledElementNumber;

	private int internalPoolSize;


	[Header("Additional Settings")]
	[SerializeField] GameObject DeadZone;

	[SerializeField] private Location currentLocation;

	[SerializeField] private float speed = 1.5f;

	private GameObject currentlyUsingObject;

	private Vector3 defaultSpawnerPosition = new Vector3(0f, 3f, 0f);

	private bool isActivated = true;

	private float cooldownTime = 1f;

	private float nextDropTime;

	private int reservPoolIndex;

	private bool isReserveObjectAdded;

	private bool isPooled = false;

	public bool isSpeedRaised ;



	#region Unity Methods
	private void OnEnable()
	{
		EventManager.Instance.AddListener<OnLevelChangedEvent>(OnLevelChangedEventHandler);
		EventManager.Instance.AddListener<OnDeadZoneEnterEvent>(OnDeadZoneEnterEventHandler);
		EventManager.Instance.AddListener<OnLocationChangedEvent>(OnLocationChangedEventHandler);
	}
	private void OnDisable()
	{
		EventManager.Instance.AddListener<OnLevelChangedEvent>(OnLevelChangedEventHandler);
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
		internalPoolSize = poolSize;
		
		for (int i = 0; i < locationList.Count; i++)
		{
			componentsDictionary.Add(locationList[i].LocationName, locationList[i].LocationsTostComponents);
		}
		///***Location Index***///
		//if (PlayerPrefs.GetInt("LocationID"))
		//{
		//	PlayerPrefs.SetInt("LocationID", 1);
		//	currentLocation = (Location)PlayerPrefs.GetInt("LocationID");
		//}
		//else if (PlayerPrefs.GetInt("LocationID")>1 && currentLocation == Location.WOODLANDIA)
		//{
		//	PlayerPrefs.SetInt("LocationID", 0);
		//	currentLocation = (Location)PlayerPrefs.GetInt("LocationID");
		//}
		if (isSpeedRaised)
		{
			speed = PlayerPrefs.GetFloat("ToastSpeed");
		}
		else
		{
			speed += 0.5f;
		}
		
		currentLocation = (Location)PlayerPrefs.GetInt("LocationID");

	}
	private void Update()
	{
		LocationID = PlayerPrefs.GetInt("LocationID");

		SpawnerMove();
		MakeVisible();
		if (!isPooled)
		{
			PoolingProcess();
			isPooled = true;
			pooledElementNumber = mainPool.Count / poolSize;
		}

		DeactivateTostComponent();

		if (Input.GetMouseButtonDown(0) && Time.time > nextDropTime)
		{
			//CooldDown process
			nextDropTime = Time.time + cooldownTime;

			currentlyUsingObject.transform.SetParent(null);
			StartCoroutine(MakeDynamic(currentlyUsingObject));
			SpawnerPositionChange(currentlyUsingObject.transform.localScale.y, "Up");
			mainPool.Remove(currentlyUsingObject);
			reservPool.Add(currentlyUsingObject);
			isReserveObjectAdded = true;
			StartCoroutine(ActivateTostCompanent());
			currentlyUsingObject.GetComponent<Rigidbody>().velocity = new Vector3(0,-1f,0);
			//DeadZone.transform.position = new Vector3(currentlyUsingObject.transform.position.x,DeadZone.transform.position.y,DeadZone.transform.position.z);
			EventManager.Instance.Raise(new OnTostComponentDropsEvent());

			//speed += 0.02f;
			
			isActivated = true;
		}
	}
	#endregion

	private void PoolingProcess()
	{//if need to add new location just create scriptable object and case for location//
		switch (currentLocation)
		{
			case Location.TOAST:
				mainPool.Clear();
				foreach (GameObject obj in componentsDictionary["Toast"])
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

	private void SpawnerPositionChange(float height, string direction)
	{
		//Raises Spawner's positioan by spawned objects localScale.y//

		if (direction == "Up")
		{
			//***Remove 0.5f after adding real prefabs***//
			transform.position += new Vector3(0, height , 0);
			DeadZone.transform.position += new Vector3(0f, height, 0f);
			Debug.Log(transform.position.y);
		}
		else if (direction == "Down")
		{
			transform.position -= new Vector3(0, height, 0);
			DeadZone.transform.position -= new Vector3(0f, height, 0f);
			Debug.Log(transform.position.y);
		}
	}

	private void SpawnerMove()
	{
		//Makes Spawner move horizontally
		//transform.position = new Vector3((Mathf.PingPong(Time.time*speed , 10) - 5), transform.position.y, transform.position.z);

		transform.position = new Vector3(Mathf.Sin(Time.time*speed)*5, transform.position.y, transform.position.z);

	}

	private void MakeVisible()
	{
		if (isActivated && mainPool.Count != 0)
		{
			StartCoroutine(MakeVisibleDelay());
		}

	}

	private void DeactivateTostComponent()
	{
		if (reservPool.Count >= 10 && isReserveObjectAdded)
		{
			GameObject componentToDeactivate = reservPool[reservPoolIndex];
			componentToDeactivate.GetComponentInChildren<Rigidbody>().isKinematic = true;
			reservPoolIndex++;
			isReserveObjectAdded = false;
			
		}
	}
	private IEnumerator ActivateTostCompanent()
	{ GameObject currentlyDropedObject = currentlyUsingObject;

		currentlyDropedObject.GetComponent<BoxCollider>().enabled = true;
		yield return new WaitForSeconds(00.2f);
	}

	private IEnumerator MakeDynamic(GameObject obj)
	{
		yield return new WaitForFixedUpdate();
		obj.GetComponent<Rigidbody>().isKinematic = false;
	}

	private IEnumerator MakeVisibleDelay()
	{
		isActivated = false;
		currentlyUsingObject = mainPool[ToastIndex(mainPool.Count)];
		currentlyUsingObject.SetActive(true);
		currentlyUsingObject.transform.position = transform.position;
		yield return new WaitForEndOfFrame();
		
		
	}
	private int ToastIndex(int poolCapacity)
	{
		//Debug.Log("PoolCapacity: " + poolCapacity + ", PooledObjectsNumber:  " + poolingSize);
		if (pooledElementNumber!=poolCapacity)
		{
			int res=(internalPoolSize * poolIndex) - poolIndex;

			poolIndex++;
			
			if (poolIndex==pooledElementNumber)
			{
				internalPoolSize--;
				poolIndex = 0;
			}

			//Debug.Log("PoledElementsNumber:" + pooledElementNumber);
			return res;
		}
		else
		{
			//Debug.Log("PoledElementsNumber:" + pooledElementNumber);
			return 0;
		}
		
	}
	private void OnLevelChangedEventHandler(OnLevelChangedEvent eventDetails)
	{
		if (eventDetails.SpawnerSpeed == 0)
		{
			speed = 1.5f;
		}
		else
		{
			speed += eventDetails.SpawnerSpeed;
		}
		PlayerPrefs.SetFloat("TostSpeed", speed);
		isSpeedRaised = true;

	}
	private void OnDeadZoneEnterEventHandler(OnDeadZoneEnterEvent eventDetails)
	{
		SpawnerPositionChange(eventDetails.LoweringHeight, "Down");
	}
	private void OnLocationChangedEventHandler(OnLocationChangedEvent eventDetails)
	{
		//locationIndex = eventDetails.locationID;
		//currentLocation = (Location)eventDetails.locationID;
		PlayerPrefs.SetInt("LocationID",eventDetails.locationID);
	}
	private enum Location
	{
		TOAST,
		WOODLANDIA,
		MIX
	}
}

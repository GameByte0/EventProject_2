using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project_Tost.Controllers
{
	public class SpawnerController : MonoBehaviour
	{
		[SerializeField] private Dictionary<string, GameObject[]> allComponents;

		[SerializeField] private GameObject[] tostComponents;

		[SerializeField] private List<GameObject> tostPool;

		[SerializeField] private int poolSize;

		[SerializeField] private int speed;

		private GameObject currentObject;

		#region Unity Methods
		private void Awake()
		{
			tostPool = new List<GameObject>();

			foreach (GameObject tost in tostComponents)
			{
				for (int i = 0; i < poolSize; i++)
				{
					GameObject tostInsctance = Instantiate(tost, transform.position, transform.rotation);
					tostInsctance.SetActive(false);

					tostPool.Add(tostInsctance);

				}
			}

		}
		private void Update()
		{
			SpawnerMove();

			if (Input.GetKeyDown(KeyCode.Space))
			{
				currentObject = tostPool[Random.Range(0, tostPool.Count)];



				if (!currentObject.activeInHierarchy && tostPool.Count != 0)
				{
					currentObject.SetActive(true);
					StartCoroutine(MakeDynamic(currentObject));
					tostPool.Remove(currentObject);
					currentObject.transform.position = transform.position;
					SpawnerPositionChange(currentObject.transform.localScale.y);
				}
			}

		}
		#endregion

		private IEnumerator MakeDynamic(GameObject obj)
		{
			yield return new WaitForSeconds(0.2f);
			obj.GetComponent<Rigidbody>().isKinematic = false;
		}

		private void SpawnerPositionChange(float height)
		{
			Debug.Log("Rise by: "+height);
			transform.position += new Vector3(0, height, 0);
		}

		private void SpawnerMove()
		{
			transform.position = new Vector3(Mathf.PingPong(Time.time*speed, 10)-5, transform.position.y, transform.position.z);
		}

	}
}


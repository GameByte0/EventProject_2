using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SpawnerController : MonoBehaviour
{
	[SerializeField] private GameObject[] tostComponents;

	[SerializeField] private List<GameObject> tostPool;

	[SerializeField] private int poolSize;

	private GameObject currentObject;

	private void Awake()
	{
		tostPool = new List<GameObject>();

		foreach (GameObject tost in tostComponents)
		{
			for (int i = 0; i < poolSize; i++)
			{
				GameObject tostInsctance = Instantiate(tost, transform);
				tostInsctance.transform.SetParent(transform);
				tostInsctance.SetActive(false);

				tostPool.Add(tostInsctance);

			}
		}

	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			currentObject = tostPool[Random.Range(0, tostPool.Count)];

			if (!currentObject.activeInHierarchy)
			{ 
				currentObject.SetActive(true);
				StartCoroutine(MakeDynamic(currentObject));
			}
			

		}
	}
	private IEnumerator MakeDynamic(GameObject obj)
	{
		yield return new WaitForSeconds(0.2f);
		obj.GetComponent<Rigidbody>().isKinematic = false;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ CreateAssetMenu (fileName ="NewLocation" , menuName ="ScriptableObject/Locations")]
public class LocationsSO : ScriptableObject
{
  [SerializeField] public string LocationName;

  [SerializeField] public List<GameObject> LocationsTostComponents;
}

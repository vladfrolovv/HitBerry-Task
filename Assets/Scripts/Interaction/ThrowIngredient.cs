using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowIngredient : MonoBehaviour {
  [Header("Blender")]
  [SerializeField] private Transform throwFrom;

  public void Throw(GameObject ingredient) {
    ingredient.transform.position = throwFrom.position;
  }
}

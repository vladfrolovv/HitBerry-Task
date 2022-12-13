using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blender : MonoBehaviour {
  [Header("Lid")]
  [SerializeField] private BlenderLid blenderLid;

  private const int MAX_INGREDIENTS_IN_BLENDER = 8;
  private readonly List<GameObject> ingredientsInBlender = new List<GameObject>();
  public void Blend() {
    blenderLid.LidState(false);
  }
  
  public bool BlenderOverflow() {
    return ingredientsInBlender.Count >= MAX_INGREDIENTS_IN_BLENDER;
  }

  public void NewBlenderIngredient(GameObject ingredient) {
    ingredientsInBlender.Add(ingredient);
  }
  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour, IIngredient, IThrowable {
  [Header("Color")]
  [SerializeField] private Color _ingredientColor;

  [Header("Ingredient")]
  [SerializeField] private GameObject ingredient;
  
  public Color ingredientColor { get => _ingredientColor; set => _ingredientColor = value; }

  public GameObject GetIngredient() {
    GameObject thisIngredient = Instantiate(ingredient);
    thisIngredient.AddComponent<Rigidbody>();
    thisIngredient.transform.localScale = Vector3.one;
    
    return thisIngredient;
  }

  public GameObject Throw() {
    return GetIngredient();
  }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ingredient : MonoBehaviour, IIngredient, IThrowable, IBlendable {
  [Header("Color")]
  [SerializeField] private Color _ingredientColor;

  [Header("Ingredient")]
  [SerializeField] private GameObject ingredient;
  
  public Color ingredientColor { get => _ingredientColor; set => _ingredientColor = value; }

  public GameObject GetIngredient() {
    GameObject thisIngredient = Instantiate(ingredient);
    
    StartCoroutine(ingredientAppear = IngredientAppear(thisIngredient));
    return thisIngredient;
  }

  private IEnumerator ingredientAppear;
  private IEnumerator IngredientAppear(GameObject thisIngredient) {
    return Animator.Animate((float t) => {
      t = Easings.easeOutBack(t);
      thisIngredient.transform.localScale = Animator.Lerp(Vector3.zero, Vector3.one, t);
    }, .06f, () => {    
      thisIngredient.AddComponent<Rigidbody>();
    });
  }

  public void Blend() {
    Destroy(gameObject.GetComponent<Rigidbody>());
    StartCoroutine(Animator.Animate((float t) => {
      t = Easings.easeOutBack(t);
      gameObject.transform.localScale = Animator.Lerp(Vector3.one, Vector3.zero, t);
    }, 2f, () => {    
      Destroy(gameObject);
    }));
  }
  
  public GameObject Throw() {
    return GetIngredient();
  }
}

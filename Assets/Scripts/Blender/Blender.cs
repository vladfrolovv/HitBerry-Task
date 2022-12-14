using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blender : MonoBehaviour {
  private const int MAX_INGREDIENTS_IN_BLENDER = 10;
  private readonly List<GameObject> ingredientsInBlender = new List<GameObject>();

  [Header("References")]
  [SerializeField] private BlenderLid blenderLid;
  [SerializeField] private BlenderShake blenderShake;
  [SerializeField] private LevelConstructor levelConstructor;

  [Header("Filler")]
  [SerializeField] private GameObject blenderFiller;
  [SerializeField] private MeshRenderer blenderFillerMeshRenderer;


  private Vector3 standardFillerScale;
  private Vector3 blenderFillerScale => blenderFiller.transform.localScale;
  private void Start() {
    standardFillerScale = blenderFillerScale;
    blenderFiller.transform.localScale = Vector3.zero;
  }

  public void Blend() {
    if (ingredientsInBlender.Count == 0) { return; }
    
    blenderLid.LidState(false); // close lid
    StartCoroutine(Animator.WithTimeout(() => {
      StartCoroutine(FillBlender());
    }, .1f)); // start filling
    
    blenderFillerMeshRenderer.materials[0].color = GetBlendColor(); // set color
    RemoveAllIngredientsFromBlender(); // remove ingredients

    blenderShake.enabled = true;
    blenderShake.InduceStress(1); // shake blender

    StartCoroutine(Animator.WithTimeout(() => {
      if (levelConstructor.LevelPassed(GetBlendColor())) 
        levelConstructor.NextLevel();
      
      ClearBlender();
    }, 2f));

  }

  private IEnumerator FillBlender() {
    Vector3 scaleFrom = blenderFillerScale;
    return Animator.Animate((float t) => {
      t = Easings.easeOutQuint(t);
      blenderFiller.transform.localScale = Animator.Lerp(scaleFrom, standardFillerScale, t);
    }, 1.4f, () => { });
  }

  private void RemoveAllIngredientsFromBlender() {
    foreach (GameObject ingredient in ingredientsInBlender) {
      if (ingredient.GetComponent<IBlendable>() != null) {
        ingredient.GetComponent<IBlendable>().Blend();
      }
    }
  }

  private Color GetBlendColor() {
    float r = 0, g = 0, b = 0, a = 0;
    foreach (GameObject ingredient in ingredientsInBlender) {
      IIngredient ingredientInterface = ingredient.GetComponent<IIngredient>();
      if (ingredientInterface != null) {
        r += ingredientInterface.ingredientColor.r;
        g += ingredientInterface.ingredientColor.g;
        b += ingredientInterface.ingredientColor.b;
        a += ingredientInterface.ingredientColor.a;
      }
    }
    return new Color(
      r / ingredientsInBlender.Count, 
      g / ingredientsInBlender.Count, 
      b / ingredientsInBlender.Count, 
      a  / ingredientsInBlender.Count
    );
  }

  public void ClearBlender() {
    blenderLid.LidState(true); 
    RemoveAllIngredientsFromBlender();
    
    Vector3 scaleFrom = blenderFillerScale;
    StartCoroutine(Animator.Animate((float t) => {
      t = Easings.easeOutQuint(t);
      blenderFiller.transform.localScale = Animator.Lerp(scaleFrom, Vector3.zero, t);
    }, 1f, () => { }));
    ingredientsInBlender.Clear();
  }
  
  public bool BlenderOverflow() {
    return ingredientsInBlender.Count >= MAX_INGREDIENTS_IN_BLENDER;
  }

  public void NewBlenderIngredient(GameObject ingredient) {
    ingredientsInBlender.Add(ingredient);
  }
  
}

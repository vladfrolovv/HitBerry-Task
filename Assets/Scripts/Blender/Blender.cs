using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Blender : MonoBehaviour {
  [Header("Lid")]
  [SerializeField] private BlenderLid blenderLid;

  [Header("Filler")]
  [SerializeField] private GameObject blenderFiller;

  private Vector3 standardFillerScale;
  private Vector3 blenderFillerScale => blenderFiller.transform.localScale;
  private void Start() {
    standardFillerScale = blenderFillerScale;
    blenderFiller.transform.localScale = Vector3.zero;
  }

  private const int MAX_INGREDIENTS_IN_BLENDER = 8;
  private readonly List<GameObject> ingredientsInBlender = new List<GameObject>();
  public void Blend() {
    blenderLid.LidState(false);
    StartCoroutine(FillBlender());
  }

  private IEnumerator FillBlender() {
    Vector3 scaleFrom = blenderFillerScale;
    return Animator.Animate((float t) => {
      t = Easings.easeOutQuint(t);
      blenderFiller.transform.localScale = Animator.Lerp(scaleFrom, standardFillerScale, t);
    }, .8f, () => { });
  }
  
  public bool BlenderOverflow() {
    return ingredientsInBlender.Count >= MAX_INGREDIENTS_IN_BLENDER;
  }

  public void NewBlenderIngredient(GameObject ingredient) {
    ingredientsInBlender.Add(ingredient);
  }
  
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IngredientInteraction : MonoBehaviour, IInteractive {

  private Vector3 standardScale;
  private void Start() {
    standardScale = transform.localScale;
  }
  
  private IEnumerator touchAnimation, releaseAnimation;
  public void OnTouch() {
    if (touchAnimation != null) 
      StopCoroutine(touchAnimation);
    
    StartCoroutine(touchAnimation = Animator.Animate((float t) => {
      t = Easings.easeOutQuint(t);
      transform.localScale = Animator.Lerp(standardScale, standardScale * .85f, t);
    }, .1f, () => {
      touchAnimation = null;
      if (releaseAnimation != null)
        StartCoroutine(releaseAnimation);
    }));
  }

  public void OnRelease() {
    Vector3 from = transform.localScale;
    releaseAnimation = Animator.Animate((float t) => {
      t = Easings.easeOutBack(t);
      transform.localScale = Animator.Lerp(from, standardScale, t);
    }, .1f, () => { releaseAnimation = null; });

    if (touchAnimation == null)
      StartCoroutine(releaseAnimation);
  }
}

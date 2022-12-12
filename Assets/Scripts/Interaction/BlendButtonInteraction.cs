using UnityEngine;
using System.Collections;

public class BlendButtonInteraction : MonoBehaviour, IInteractive {
  [Header("Button Transform")]
  [SerializeField] private Transform buttonTransform;

  private Vector3 scaleFrom => buttonTransform.localScale;
  private readonly Vector3 standardScale = Vector3.one;
  private IEnumerator blendButtonPress, blendButtonRelease;
  public void OnTouch() {
    if (blendButtonPress != null)
      StopCoroutine(blendButtonPress);

    Vector3 scaleFrom = this.scaleFrom;
    StartCoroutine(blendButtonPress = Animator.Animate((float t) => {
      t = Easings.easeOutQuint(t);
      buttonTransform.localScale = Animator.Lerp(scaleFrom, standardScale * .9f, t);
    }, .2f, () => {
      blendButtonPress = null;
      if (blendButtonRelease != null)
        StartCoroutine(blendButtonRelease);
    }));

  }

  public void OnRelease() {
    Vector3 scaleFrom = this.scaleFrom;
    blendButtonRelease = Animator.Animate((float t) => {
      t = Easings.easeOutBack(t);
      buttonTransform.localScale = Animator.Lerp(scaleFrom, standardScale, t);
    }, .2f, () => { });
    
    if (blendButtonPress == null)
      StartCoroutine(blendButtonRelease);
  }
}

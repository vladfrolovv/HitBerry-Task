using UnityEngine;
using System.Collections;

public class BlenderLid : MonoBehaviour {
  [Header("Lid Transform")]
  [SerializeField] private Transform lidTransform;

  private void Start() {
    LidState(true);
  }
  
  private IEnumerator lidStatusChangeAnimation;
  public void LidState(bool setOpen) {
    lidOpen = setOpen;
      
    if (lidStatusChangeAnimation != null)
      StopCoroutine(lidStatusChangeAnimation);

    if (lidTransform == null)
      return;
    
    Vector3 startEulerAngles = lidTransform.localEulerAngles;
    Vector3 finalEulerAngles = setOpen ? new Vector3(0, 0, 90) : Vector3.zero; 
    StartCoroutine(lidStatusChangeAnimation = Animator.Animate((float t) => {
      t = setOpen ? Easings.easeOutQuint(t) : Easings.easeOutBounce(t);
      lidTransform.localEulerAngles = Animator.Lerp(startEulerAngles, finalEulerAngles, t);
    }, setOpen ? .25f : .4f, () => { }));
  }

  private bool lidOpen = true; // open?
  public bool LidOpen() {
    return lidOpen;
  }
  
}

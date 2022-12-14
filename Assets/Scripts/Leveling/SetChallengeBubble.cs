using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetChallengeBubble : MonoBehaviour {
  [Header("Bubble")]
  [SerializeField] private Image bubbleImage;
  [SerializeField] private RectTransform bubbleRectTransform;

  private Vector3 standardBubbleScale;
  private void Start() {
    standardBubbleScale = bubbleRectTransform.localScale;
    bubbleRectTransform.localScale = Vector3.zero;
  }
  
  public void SetNewBubble(Color color) {
    bubbleImage.color = new Color(color.r, color.g, color.b, 1);
    StartCoroutine(BubbleStatus(false, true));
  }

  private IEnumerator BubbleStatus(bool setActive, bool reset) {
    Vector3 scaleFrom = bubbleRectTransform.localScale;
    return Animator.Animate((float t) => {
      t = Easings.easeOutQuint(t);
      bubbleRectTransform.localScale = Animator.Lerp(scaleFrom, setActive ? standardBubbleScale : Vector3.zero, t);
    }, .2f, () => {
      if (reset)
        StartCoroutine(BubbleStatus(true, false));
    });
  }
}

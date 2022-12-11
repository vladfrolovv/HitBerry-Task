using System;
using UnityEngine;
using System.Collections;

public class Animator : MonoBehaviour {
  public static IEnumerator Animate(Action<float> update, float time, Action onEnd) {
    update(0);
    yield return null;
    
    float start = Time.time, t = 0;
    while ((t = (Time.time - start) / time) <= 1) {
      update(t);
      yield return null;
    }
    update(1f);
    if (onEnd != null) {
      onEnd();
    }
    yield break;
  }

  public static Vector3 Lerp(Vector3 from, Vector3 to, float t) {
    return from + (to - from) * t;
  }
}

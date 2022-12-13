using UnityEngine;

public class LevelConstructor : MonoBehaviour {
  
  private bool LevelPassed(Color desiredColor, Color resultingColor) {
    float matchingR = Mathf.Min(desiredColor.r, resultingColor.r) / Mathf.Max(desiredColor.r, resultingColor.r);
    float matchingG = Mathf.Min(desiredColor.g, resultingColor.g) / Mathf.Max(desiredColor.g, resultingColor.g);
    float matchingB = Mathf.Min(desiredColor.b, resultingColor.b) / Mathf.Max(desiredColor.b, resultingColor.b);
    
    return (matchingR + matchingG + matchingB) / 3 > .85f;
  }
  
}

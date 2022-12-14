using System.Collections.Generic;
using UnityEngine;

public class LevelConstructor : MonoBehaviour {

  [Header("Bubble")]
  [SerializeField] private SetChallengeBubble setChallengeBubble;

  [Header("Levels")]
  [SerializeField] private List<Color> levels = new List<Color>();
  
  [HideInInspector] public Color desiredColor;
  private void Start() {
    ConstructLevel(levels[0]);
  }

  private void ConstructLevel(Color desiredColor) {
    this.desiredColor = desiredColor;
    setChallengeBubble.SetNewBubble(desiredColor);
    PlayerPrefs.SetInt("last-level", levels.IndexOf(desiredColor));
  }

  public void NextLevel() {
    ConstructLevel(levels[(PlayerPrefs.GetInt("last-level") + 1) <= 0 ? 0 : 1 + PlayerPrefs.GetInt("last-level") % (levels.Count - 1)]);
  }
  
  public bool LevelPassed(Color resultingColor) {
    float matchingR = Mathf.Min(desiredColor.r, resultingColor.r) / Mathf.Max(desiredColor.r, resultingColor.r);
    float matchingG = Mathf.Min(desiredColor.g, resultingColor.g) / Mathf.Max(desiredColor.g, resultingColor.g);
    float matchingB = Mathf.Min(desiredColor.b, resultingColor.b) / Mathf.Max(desiredColor.b, resultingColor.b);
    
    return (matchingR + matchingG + matchingB) / 3 > .85f;
  }
  
}

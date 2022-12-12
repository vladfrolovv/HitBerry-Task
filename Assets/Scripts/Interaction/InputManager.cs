using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour {
  [Header("Ingredient Menu")]
  [SerializeField] private ScrollRect ingredientsMenu;

  [Header("References")]
  [SerializeField] private ThrowIngredient throwIngredient;
  
  private GameObject interactedGameObject;
  private Vector3 touchStartPos, touchEndPos;
  
  private TouchPhase phase;
  private Touch touch;
  private void Update() {
    // add possible touch zone
    if (Input.touchCount == 1 || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0)) {
      if (Input.touchCount == 1) {
        touch = Input.GetTouch(0);
        phase = touch.phase;
        if (phase == TouchPhase.Began) {
          touchStartPos = touch.position;
        } else if (phase == TouchPhase.Ended) {
          touchEndPos = touch.position;
        }
      } else {
        if (Input.GetMouseButtonDown(0)) {
          touchStartPos = Input.mousePosition;
          phase = TouchPhase.Began;
        } else if (Input.GetMouseButtonUp(0)) {
          touchEndPos = Input.mousePosition;
          phase = TouchPhase.Ended;
        }
      }

      if (phase == TouchPhase.Began) {
        Ray ray = Camera.main.ScreenPointToRay(touchStartPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
          if (hit.collider.gameObject.GetComponentInParent<IInteractive>() != null) {
            interactedGameObject = hit.collider.gameObject;
            interactedGameObject.GetComponentInParent<IInteractive>().OnTouch();
          }
        } 
      } else if (phase == TouchPhase.Ended) {
        if (interactedGameObject) {
          Ray ray = Camera.main.ScreenPointToRay(touchEndPos);
          RaycastHit hit;
          if (Physics.Raycast(ray, out hit)) {
            if (interactedGameObject == hit.collider.gameObject) {
              if (interactedGameObject.GetComponentInParent<IThrowable>() != null) {
                throwIngredient.Throw(interactedGameObject.GetComponentInParent<IThrowable>().Throw());
              } 
            }
          }
          interactedGameObject.GetComponentInParent<IInteractive>().OnRelease();
          interactedGameObject = null;
        }
      }
    } 
  }
}

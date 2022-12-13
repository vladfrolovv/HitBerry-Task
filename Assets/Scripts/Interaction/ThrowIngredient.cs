using UnityEngine;

public class ThrowIngredient : MonoBehaviour {
  [Header("Blender")]
  [SerializeField] private Transform throwFrom;

  [Header("References")]
  [SerializeField] private Blender blender;
  [SerializeField] private BlenderLid blenderLid;

  public void Throw(GameObject ingredient) {
    if (!blenderLid.LidOpen() || blender.BlenderOverflow())
      return;

    blender.NewBlenderIngredient(ingredient);
    ingredient.transform.position = throwFrom.position;
  }
}

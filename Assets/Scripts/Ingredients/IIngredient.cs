using UnityEngine;

public interface IIngredient {
  public Color GetColor { get; set; }
  public GameObject GetIngredient();
}

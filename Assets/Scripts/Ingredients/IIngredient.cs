using UnityEngine;

public interface IIngredient {
  public Color ingredientColor { get; set; }
  public GameObject GetIngredient();
}

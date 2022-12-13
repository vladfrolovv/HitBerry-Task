using UnityEngine;

public class BlenderShake : MonoBehaviour {
  [Header("Blender Transform")]
  [SerializeField] private Transform blenderTransform;

  [Header("Shake Parameters")]
  [SerializeField] private float frequency = 25;
  [SerializeField] private float traumaExponent = 2;
  [SerializeField] private float recoverySpeed = 1f;
    
  private readonly Vector3 maximumTranslationShake = Vector3.one;
  private readonly Vector3 maximumAngularShake = Vector3.one * 2;
  private float trauma, seed;

  private Vector3 standardBlenderRotation;
  private void Awake() {
    seed = Random.value;
    standardBlenderRotation = blenderTransform.transform.eulerAngles;
  }
  
  private void Update() {
    float shake = Mathf.Pow(trauma, traumaExponent);
    if (shake <= 0) { enabled = false; }

    blenderTransform.eulerAngles = new Vector3(
      maximumAngularShake.x * (Mathf.PerlinNoise(seed, Time.time * frequency) * 2 - 1),
      maximumAngularShake.y * (Mathf.PerlinNoise(seed, Time.time * frequency) * 2 - 1),
      maximumAngularShake.z * (Mathf.PerlinNoise(seed + 1, Time.time * frequency) * 2 - 1)
    ) * shake + standardBlenderRotation;

    trauma = Mathf.Clamp01(trauma - recoverySpeed * Time.deltaTime);
  }

  public void InduceStress(float stress) { 
    trauma = Mathf.Clamp01(trauma + stress);
  }
  
}

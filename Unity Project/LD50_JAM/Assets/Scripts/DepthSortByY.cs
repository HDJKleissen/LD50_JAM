using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(SpriteRenderer))]
public class DepthSortByY : MonoBehaviour
{
    private const int IsometricRangePerYUnit = 10;

    [Tooltip("Will use this object to compute z-order")]
    public Transform Target;

    [Tooltip("Use this to offset the object slightly in front or behind the Target object")]
    public int TargetOffset = 0;
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Target == null)
            Target = transform;
        spriteRenderer.sortingOrder = -Mathf.RoundToInt(Target.position.y * IsometricRangePerYUnit) * 10 + TargetOffset;
    }
}
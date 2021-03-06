using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedSpriteChooser : MonoBehaviour
{
    public bool HasPatient;

    Patient bed;
    [SerializeField] BedDirection bedDirection;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] PolygonCollider2D polygonCollider;
    [SerializeField] Sprite HorizontalEmptySprite, HorizontalFullSprite, VerticalEmptySprite, VerticalFullSprite;

    // Start is called before the first frame update
    void Start()
    {
        bed = GetComponent<Patient>();
    }

    // Update is called once per frame
    void Update()
    {
        bool previousHasPatient = HasPatient;
        HasPatient = bed.HasPatient;
        if(HasPatient != previousHasPatient)
        {
            UpdateSprite(HasPatient);
        }
    }

    void UpdateSprite(bool hasPatient)
    {
        if (hasPatient)
        {
            switch (bedDirection)
            {
                case BedDirection.HORIZONTAL:
                    spriteRenderer.sprite = HorizontalFullSprite;
                    break;
                case BedDirection.VERTICAL:
                    spriteRenderer.sprite = VerticalFullSprite;
                    break;
            }
        }
        else
        {
            switch (bedDirection)
            {
                case BedDirection.HORIZONTAL:
                    spriteRenderer.sprite = HorizontalEmptySprite;
                    break;
                case BedDirection.VERTICAL:
                    spriteRenderer.sprite = VerticalEmptySprite;
                    break;
            }
        }

        polygonCollider.UpdateShapeToSprite(spriteRenderer.sprite);
    }

    private void OnValidate()
    {
        UpdateSprite(HasPatient);
    }

    enum BedDirection
    {
        HORIZONTAL,
        VERTICAL
    }
}

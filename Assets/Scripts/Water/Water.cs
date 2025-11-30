using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [Header("Referneces")]
    [SerializeField]
    private SpriteRenderer square;
    [SerializeField]
    private BoxCollider2D boxCollider;

    [Header("Parameters")]
    [SerializeField]
    private float coolingSpeed;

    [Header("Palette")]
    [SerializeField]
    private Color[] palette;

    private int temperature;

    public void HeatUp()
    {
        temperature += 1;

        if(temperature == 5)
        {
            boxCollider.enabled = false;
        }

        UpdateColor();

        Invoke(nameof(CoolDown), coolingSpeed);
    }

    private void UpdateColor()
    {
        square.color = palette[temperature];
    }

    private void CoolDown()
    {
        temperature -= 1;

        if(temperature == 4)
        {
            boxCollider.enabled = true;
        }

        UpdateColor();
    }
}
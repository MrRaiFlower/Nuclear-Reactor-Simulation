using Unity.Mathematics;
using UnityEngine;

public class FuelCell : MonoBehaviour
{
    [Header("Referneces")]
    [SerializeField] private GameObject circle;

    [Header("Appearence")]
    [SerializeField] private Color fullColor;
    [SerializeField] private Color depletedColor;

    [Header("Properties")]
    [SerializeField] private float xenonChance;

    [Header("Prefabs")]
    [SerializeField] private GameObject neutron;
    [SerializeField] private GameObject xenon;

    public bool isExhausted;

    private void Start()
    {
        if (isExhausted)
        {
            Exhaust();
        }
    }

    public void Exhaust()
    {
        this.circle.GetComponent<SpriteRenderer>().color = depletedColor;

        this.isExhausted = true;

        this.GetComponent<Collider2D>().enabled = false;
    }

    public void SpawnXenon()
    {
        if (UnityEngine.Random.value < xenonChance)
        {
            Instantiate(xenon, this.transform.position + Vector3.back, quaternion.identity);
        }
    }

    public void Refill()
    {
        this.circle.GetComponent<SpriteRenderer>().color = fullColor;

        this.isExhausted = false;
        
        this.GetComponent<Collider2D>().enabled = true;
    }

    public void CreateNeutrons()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(createNeutron(), this.transform.position, quaternion.identity);
        }
    }

    private GameObject createNeutron()
    {
        GameObject newNeutron = neutron;

        newNeutron.GetComponent<Neutron>().startingDirection = Quaternion.AngleAxis(UnityEngine.Random.Range(0.0f, 1.0f) * 360.0f, Vector3.forward) * Vector3.up;
        newNeutron.GetComponent<Neutron>().startingSpeed = true;

        return newNeutron;
    }
}

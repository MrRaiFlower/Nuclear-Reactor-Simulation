using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class FuelCell : MonoBehaviour
{
    [Header("Referneces")]
    [SerializeField] private GameObject circle;

    [Header("Appearence")]
    [SerializeField] private Color depletedColor;

    [Header("Parameters")]
    [SerializeField] private int numberOfNeutrons;

    [Header("Prefabs")]
    [SerializeField] private GameObject neutron;

    public bool isExhausted;

    void Start()
    {
        if (isExhausted)
        {
            Exhaust();
        }
    }

    public void Exhaust()
    {
        circle.GetComponent<SpriteRenderer>().color = depletedColor;
        
        this.GetComponent<Collider2D>().enabled = false;

        Destroy(this);
    }

    public void CreateNeutrons()
    {
        for (int i = 0; i < numberOfNeutrons; i++)
        {
            Instantiate(createNeutron(), this.transform.position, quaternion.identity);
        }
    }

    private GameObject createNeutron()
    {
        GameObject newNeutron = neutron;

        newNeutron.GetComponent<Neutron>().startingDirection = Quaternion.AngleAxis(UnityEngine.Random.Range(0.0f, 1.0f) * 360.0f, Vector3.forward) * Vector3.up;
        newNeutron.GetComponent<Neutron>().startingSpeed = UnityEngine.Random.value > 0.5f;

        return newNeutron;
    }
}

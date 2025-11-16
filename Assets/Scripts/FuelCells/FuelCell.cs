using Unity.Mathematics;
using UnityEngine;

public class FuelCell : MonoBehaviour
{
    [Header("Referneces")]
    [SerializeField] private GameObject circle;

    [Header("Appearence")]
    [SerializeField] private Color depletedColor;

    [Header("Parameters")]
    [SerializeField] private int neutronsNumber;
    [SerializeField] private float neutronsMinSpeed;
    [SerializeField] private float neutronsMaxSpeed;

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
        for (int i = 0; i < neutronsNumber; i++)
        {
            Instantiate(createNeutron(), this.transform.position, quaternion.identity);
        }
    }

    private GameObject createNeutron()
    {
        GameObject newNeutron = neutron;

        newNeutron.GetComponent<Neutron>().startingVelocity = Quaternion.AngleAxis(UnityEngine.Random.Range(0.0f, 1.0f) * 360.0f, Vector3.forward) * Vector3.up;
        newNeutron.GetComponent<Neutron>().startingSpeed = UnityEngine.Random.Range(neutronsMinSpeed, neutronsMaxSpeed);

        return newNeutron;
    }
}

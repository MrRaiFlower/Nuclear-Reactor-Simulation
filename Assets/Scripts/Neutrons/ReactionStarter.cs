using UnityEngine;

public class ReactionStarter : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float neutronsMinSpeed;
    [SerializeField] private float neutronsMaxSpeed;

    [Header("Prefabs")]
    [SerializeField] private GameObject neutron;

    void Start()
    {
        GameObject newNeutron = neutron;

        newNeutron.GetComponent<Neutron>().startingVelocity = Quaternion.AngleAxis(UnityEngine.Random.Range(0.0f, 1.0f) * 360.0f, Vector3.forward) * Vector3.up;
        newNeutron.GetComponent<Neutron>().startingSpeed = UnityEngine.Random.Range(neutronsMinSpeed, neutronsMaxSpeed);

        Instantiate(newNeutron);
    }
}

using UnityEngine;

public class ReactionStarter : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private int numberOfNeutrons;

    [Header("Prefabs")]
    [SerializeField] private GameObject neutron;

    void Start()
    {
        for (int i = 0; i < numberOfNeutrons; i++)
        {
            GameObject newNeutron = neutron;

            newNeutron.GetComponent<Neutron>().startingDirection = Quaternion.AngleAxis(UnityEngine.Random.Range(0.0f, 1.0f) * 360.0f, Vector3.forward) * Vector3.up;
            newNeutron.GetComponent<Neutron>().startingSpeed = false;

            Instantiate(newNeutron);
        }
    }
}

using UnityEngine;

public class FuellLoader : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private int cellsHorizontally;
    [SerializeField] private int cellsVertically;
    [SerializeField] private float step;
    [SerializeField] [Range(0.0f, 1.0f)] private float loadRation;

    [Header("Prefabs")]
    [SerializeField] private GameObject fuelCell;

    void Start()
    {
        for (int i = 0; i < cellsVertically; i++)
        {
            for (int j = 0; j < cellsHorizontally; j++)
            {
                GameObject newFuelCell = fuelCell;

                newFuelCell.GetComponent<FuelCell>().isExhausted = UnityEngine.Random.value + 0.01f > 1.0f - loadRation;

                Instantiate(newFuelCell, this.transform.position + new Vector3(step * j, step * i, 0.0f), Quaternion.identity, this.gameObject.transform);
            }
        }
    }
}

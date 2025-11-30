using System.Collections.Generic;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

public class FuellLoader : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private int cellsHorizontally;
    [SerializeField] private int cellsVertically;
    [SerializeField] private float step;
    [SerializeField] [Range(0.0f, 1.0f)] private float loadRation;
    [SerializeField] private float refillSpeed;

    [Header("Prefabs")]
    [SerializeField] private GameObject fuelCell;
    [SerializeField] private GameObject water;

    private float refillProgress;

    private void Start()
    {
        for (int i = 0; i < cellsVertically; i++)
        {
            for (int j = 0; j < cellsHorizontally; j++)
            {
                Instantiate(water, this.transform.position + new Vector3(step * j, step * i, 0.0f), Quaternion.identity);

                GameObject newFuelCell = fuelCell;

                newFuelCell.GetComponent<FuelCell>().isExhausted = Random.value > loadRation;

                Instantiate(newFuelCell, this.transform.position + new Vector3(step * j, step * i, 0.0f), Quaternion.identity, this.gameObject.transform);
            }
        }
    }

    private void Update()
    {
        refillProgress += Time.deltaTime * refillSpeed;

        if (refillProgress >= 1.0f)
        {
            again:

            int childIndex = Random.Range(0, this.transform.childCount);

            Transform randomChildTransform = this.transform.GetChild(childIndex);

            if (randomChildTransform.gameObject.GetComponent<FuelCell>().isExhausted)
            {
                randomChildTransform.gameObject.GetComponent<FuelCell>().Refill();

                refillProgress -= 1.0f;

                if (refillProgress >= 1.0f)
                {
                    goto again;
                }
            }
            else
            {
                goto again;
            }
        }
    }
}

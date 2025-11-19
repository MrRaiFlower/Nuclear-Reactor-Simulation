using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;

public class Neutron : MonoBehaviour
{
    [Header("Referneces")]
    [SerializeField] private GameObject sprite;

    [Header("Appearence")]
    [SerializeField] private Color color;

    [Header("Parameters")]
    [SerializeField] private float radius;
    public Vector3 startingDirection;
    public bool startingSpeed;

    private Vector3 direction;
    [HideInInspector] public bool isFast;

    void Start()
    {
        setParameters(startingDirection, startingSpeed);
    }

    public void setParameters(Vector3 newDirection, bool newSpeed)
    {
        direction = newDirection;
        isFast = newSpeed;
    }

    void FixedUpdate()
    {
        RaycastHit2D hit;
        
        hit = Physics2D.Raycast(this.transform.position, Vector2.up, radius);

        if (!hit)
        {
            hit = Physics2D.Raycast(this.transform.position, Vector2.down, radius);
        }

        if (hit)
        {
            Collide(hit);

            if (hit.collider.tag != "Fuel Cell")
            {
                direction.y *= -1.0f;
            }
            else if (isFast)
            {
                Destroy(this.gameObject);
            }

            goto move;
        }

        hit = Physics2D.Raycast(this.transform.position, Vector2.right, radius);

        if (!hit)
        {
            hit = Physics2D.Raycast(this.transform.position, Vector2.left, radius);
        }

        if (hit)
        {
            Collide(hit);

            if (hit.collider.tag != "Fuel Cell")
            {
                direction.x *= -1.0f;
            }
            else if (isFast)
            {
                Destroy(this.gameObject);
            }

            goto move;
        }

        move:

        if (this.isFast)
        {
            this.transform.position += direction.normalized * 2.0f * Time.fixedDeltaTime;
        }
        else
        {
            this.transform.position += direction.normalized * 1.0f * Time.fixedDeltaTime;
        }
        
    }

    void Collide(RaycastHit2D hit)
    {
        switch (hit.collider.tag)
        {
            case "Frame":

                break;
            
            case "Fuel Cell":

                if (isFast)
                {
                    hit.collider.gameObject.GetComponent<FuelCell>().CreateNeutrons();
                    hit.collider.gameObject.GetComponent<FuelCell>().Exhaust();
                }

                break;
            
            case "Control Rod":

                if (this.isFast)
                {
                    this.isFast = false;
                }
                else
                {
                    Destroy(this.gameObject);
                }

                break;
            
            case "Moderator":

                this.isFast = true;

                break;
        }
    }
}

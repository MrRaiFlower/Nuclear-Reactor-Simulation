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
    public Vector3 startingVelocity;
    public float startingSpeed;

    [Header("Tags")]
    private const string FrameTag = "Frame";
    private const string FuelCellTag = "Fuel Cell";

    private Vector3 velocity;
    private float speed;

    void Start()
    {
        setParameters(startingVelocity, startingSpeed);
    }

    public void setParameters(Vector3 newVelocity, float newSpeed)
    {
        velocity = newVelocity;
        speed = newSpeed;
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

            if (hit.collider.tag != FuelCellTag)
            {
                velocity.y *= -1.0f;
            }
            else
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

            if (hit.collider.tag != FuelCellTag)
            {
                velocity.x *= -1.0f;
            }
            else
            {
                Destroy(this.gameObject);
            }

            goto move;
        }

        move:

        this.transform.position += velocity.normalized * speed * Time.fixedDeltaTime;
    }

    void Collide(RaycastHit2D hit)
    {
        switch (hit.collider.tag)
        {
            case FrameTag:

                break;
            
            case FuelCellTag:

                hit.collider.gameObject.GetComponent<FuelCell>().CreateNeutrons();
                hit.collider.gameObject.GetComponent<FuelCell>().Exhaust();

                break;
        }
    }

    public Neutron(Vector3 startingVelocity, float startingSpeed)
    {
        this.velocity = startingVelocity;
        this.speed = startingSpeed;
    }
}

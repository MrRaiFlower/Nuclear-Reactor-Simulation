using UnityEngine;

public class Neutron : MonoBehaviour
{
    [Header("Referneces")]
    [SerializeField] private GameObject sprite;

    [Header("Parameters")]
    [SerializeField] private float radius;
    public Vector3 startingDirection;
    public bool startingSpeed;
    [SerializeField] private float chanceToHeatUp;

    [Header("Appearence")]
    [SerializeField] private Color fastColor;
    [SerializeField] private Color slowColor;

    private Vector3 direction;
    [HideInInspector] public bool isFast;

    void Start()
    {
        setParameters(startingDirection, startingSpeed);

        UpdateColor();
    }

    private void UpdateColor()
    {
        if (isFast)
        {
            sprite.GetComponent<SpriteRenderer>().color = fastColor;
        }
        else
        {
            sprite.GetComponent<SpriteRenderer>().color = slowColor;
        }
    }

    public void setParameters(Vector3 newDirection, bool newSpeed)
    {
        direction = newDirection;
        isFast = newSpeed;
    }

    void FixedUpdate()
    {
        RaycastHit2D hit;
        
        hit = Physics2D.Raycast(this.transform.position, Vector2.up, radius, ~LayerMask.GetMask("Ignore"));

        if (!hit)
        {
            hit = Physics2D.Raycast(this.transform.position, Vector2.down, radius, ~LayerMask.GetMask("Ignore"));
        }

        if (hit)
        {
            Collide(hit);

            if (hit.collider.tag != "Fuel Cell" && hit.collider.tag != "Xenon" && hit.collider.tag != "Moderator")
            {
                direction.y *= -1.0f;
            }
            else if (isFast)
            {
                Destroy(this.gameObject);
            }

            goto move;
        }

        hit = Physics2D.Raycast(this.transform.position, Vector2.right, radius, ~LayerMask.GetMask("Ignore"));

        if (!hit)
        {
            hit = Physics2D.Raycast(this.transform.position, Vector2.left, radius, ~LayerMask.GetMask("Ignore"));
        }

        if (hit)
        {
            Collide(hit);

            if (hit.collider.tag != "Fuel Cell" && hit.collider.tag != "Xenon" && hit.collider.tag != "Moderator")
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

        RaycastHit2D waterHit = Physics2D.Raycast(this.transform.position, Vector2.up, radius / 0.5f, LayerMask.GetMask("Ignore"));

        if (waterHit)
        {
            if (Random.value < chanceToHeatUp)
                {
                    waterHit.transform.gameObject.GetComponent<Water>().HeatUp();

                    if (isFast)
                    {
                        isFast = false; UpdateColor();
                    }
                    else
                    {
                        Destroy(this.gameObject);
                    }
                }
        }
    }

    void Collide(RaycastHit2D hit)
    {
        switch (hit.collider.tag)
        {
            case "Frame":

                break;
            
            case "Fuel Cell":

                if (!isFast)
                {
                    hit.collider.gameObject.GetComponent<FuelCell>().CreateNeutrons();
                    hit.collider.gameObject.GetComponent<FuelCell>().Exhaust();
                    hit.collider.gameObject.GetComponent<FuelCell>().SpawnXenon();
                }

                break;
            
            case "Control Rod":

                Destroy(this.gameObject);

                break;
            
            case "Moderator":

                this.isFast = false; UpdateColor();

                break;
            
            case "Xenon":

                hit.collider.gameObject.GetComponent<Xenon>().Deplete();
                Destroy(this.gameObject);

                break;
        }
    }
}

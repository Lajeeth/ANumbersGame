using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Base class for enemy behavior.
/// </summary>
public class EnemySwarmerBehavior : EnemyBehavior
{
    [Header("Movement Attributes")]
    [Tooltip("The starting orbit radius.")] [SerializeField] float radius = 10;
    [Tooltip("The starting orbit angle.")] [SerializeField] float angle = 0;

    float scaler = 1;
    float invokeTimeRadius = 0.1f;
    float invokeTimeScaler = 0.1f;

    float alpha = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DecreaseRadius(invokeTimeRadius));
        StartCoroutine(IncreaseScaler(invokeTimeScaler));
    }

    // Update is called once per frame
    void Update()
    {
        /*
        // Elliptical path
        transform.position = new Vector2(0f + (10f * Mathf.Sin(Mathf.Deg2Rad * alpha)), 0f + (5f * Mathf.Cos(Mathf.Deg2Rad * alpha)));
        alpha += 0.01f;
        */
        
        float x = 0;
        float y = 0;

        Vector2 direction = Vector2.zero;

        x = radius * Mathf.Cos(angle);
        y = radius * Mathf.Sin(angle);

        transform.position = new Vector2(x, y);

        angle += 15 * Mathf.Deg2Rad * Time.deltaTime * scaler;
    }

    IEnumerator DecreaseRadius(float time)
    {
        yield return new WaitForSeconds(invokeTimeRadius);
        while (true)
        {
            if (radius > 0)
                radius -= 0.01f;
            //Debug.Log("Radius: " + radius);
            if (invokeTimeRadius > 0.0002)
            {
                invokeTimeRadius -= 0.0002f;
            }
            yield return new WaitForSeconds(invokeTimeRadius);
        }
    }

    IEnumerator IncreaseScaler(float time)
    {
        yield return new WaitForSeconds(invokeTimeScaler);
        while (true)
        {
            //Debug.Log("Scaler: " + scaler);
            if (scaler < 16)
            {
                scaler += 0.01f;
            }
            if (invokeTimeScaler > 0.0002)
            {
                invokeTimeScaler -= 0.0002f;
            }
            
            yield return new WaitForSeconds(invokeTimeScaler);
        }
    }

    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            // Decrease player health by this enemy's damage stat.
            Debug.Log("Player took damage from " + this.gameObject.name);
            Destroy(this.gameObject);
        }
    }

    /*
    void ChangeRadius()
    {
        if (invokeTime > 0.0001)
        {
            invokeTime -= 0.0001f;
            Invoke("DecreaseRadius", invokeTime);
        }
    }

    void DecreaseRadius()
    {
        if (radius > 0)
        {
            radius -= 0.01f;
        }
    }

    void IncreaseScaler()
    {
        if (scaler < 16)
        {
            scaler += 0.01f;
        }
    }
    */
}

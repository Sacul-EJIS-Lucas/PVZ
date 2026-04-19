// Assets/Scripts/SunObject.cs
using UnityEngine;

public class SunObject : MonoBehaviour
{
    public float targetY;
    public float fallSpeed = 2f;
    public float lifetime = 8f;
    public int sunValue = 25;

    private bool reached = false;
    private float timer;

    void Update()
    {
        if (!reached)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                new Vector3(transform.position.x, targetY, 0),
                fallSpeed * Time.deltaTime
            );
            if (Mathf.Abs(transform.position.y - targetY) < 0.05f)
                reached = true;
        }
        else
        {
            timer += Time.deltaTime;
            if (timer >= lifetime) Destroy(gameObject);
        }
    }

    void OnMouseDown()
    {
        SunManager.Instance.AddSun(sunValue);
        Destroy(gameObject);
    }
}
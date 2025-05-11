using UnityEngine;

public class FallingObject : MonoBehaviour
{
    public float minFallSpeed = 2f, maxFallSpeed = 5f, minRotSpeed = 360f, maxRotSpeed = 720f;

    private float fallSpeed, rotSpeed;
    private float rotValue;

    private float timer = 0f;

    public float destroyTimer = 6f;
    public float destroyHeight = -6f;

    void Start()
    {
        fallSpeed = Random.Range(minFallSpeed, maxFallSpeed);
        rotSpeed = Random.Range(minRotSpeed, maxRotSpeed);
    }

    void Update()
    {
        MoveObject();
        RotateObject();
        CheckDestroyConditions();
    }

    void MoveObject()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

    void RotateObject()
    {
        rotValue += rotSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0f, 0f, rotValue);
    }

    void CheckDestroyConditions()
    {
        timer += Time.deltaTime;
        if (timer >= destroyTimer || transform.position.y <= destroyHeight)
        {
            Destroy(gameObject);
        }
    }
}
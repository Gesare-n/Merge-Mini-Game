using UnityEngine;

public class DestroyVisualPrefabs : MonoBehaviour
{
    public float destroyTime = 3.0f;
    public Vector3 randomiseTextPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        randomiseTextPosition = new Vector3(0.5f, 0, 0);
        Destroy(gameObject, destroyTime);
        transform.localPosition += new Vector3(
            Random.Range(-randomiseTextPosition.x, randomiseTextPosition.x),
            Random.Range(-randomiseTextPosition.y, randomiseTextPosition.y), 
            0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

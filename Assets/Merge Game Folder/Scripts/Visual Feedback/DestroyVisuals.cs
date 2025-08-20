using UnityEngine;

public class DestroyVisuals : MonoBehaviour
{
    public float destroyTime = 0.3f;
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }
}

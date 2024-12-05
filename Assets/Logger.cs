using UnityEngine;

public class Logger : MonoBehaviour
{
    public Vector3 size;

    private MeshRenderer renderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        size = renderer.bounds.size;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

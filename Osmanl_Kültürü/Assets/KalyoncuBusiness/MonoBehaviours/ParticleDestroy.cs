using UnityEngine;

public class ParticleDestroy : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void Start()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if (_particleSystem != null && !_particleSystem.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }
}

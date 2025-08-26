using System.Collections.Generic;
using UnityEngine;

public class GameObjectEmitter : MonoBehaviour
{

    [field: SerializeField] public GameObject ObjectPrefab { get; private set; }

    private ParticleSystem _ps;
    private List<ParticleSystem.Particle> exitParticles = new();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _ps = GetComponent<ParticleSystem>();
    }

    private void OnParticleTrigger()
    {
        _ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exitParticles);
        foreach (ParticleSystem.Particle p in exitParticles)
        {
            GameObject spawnedObject = Instantiate(ObjectPrefab);
            spawnedObject.transform.position = p.position;
        }
    }

}

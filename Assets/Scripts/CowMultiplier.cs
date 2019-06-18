using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CowMultiplier : MonoBehaviour
{
    public int Iterations;
    public GameObject CowPrefab;
    
    private float _creationTime;
    private float _minTimeBeforeDuplication = 1f;
    private bool _notDuplicatedYet = true;
    private float _averageUpwardForce = 600f;
    private AudioSource[] _audioSources;

    public void Start()
    {
        _creationTime = Time.time;
        _audioSources = GetComponents<AudioSource>();

        StartCoroutine(PlayRandomMoo());
    }

    private IEnumerator PlayRandomMoo()
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.4f));
        
        var randomIndex = Random.Range(0, _audioSources.Length - 1); 
        var audioSource = _audioSources[randomIndex];
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.Play();
    }

    public void OnCollisionEnter(Collision other)
    {
        if (enabled && CanCreate())
        {
            CreateNewCow();
            CreateNewCow();

            _notDuplicatedYet = false;
        }
    }

    private bool CanCreate()
    {
        var canCreate = Time.time > _creationTime + _minTimeBeforeDuplication 
                        && _notDuplicatedYet
                        && Iterations > 0; 
        
        return canCreate;
    }

    private void CreateNewCow()
    {
        var cow = Instantiate(CowPrefab, transform.position + new Vector3(Random.Range(-1, 1), 2, Random.Range(-1, 1)), Quaternion.identity, transform);
        
        var cowRigidbody = cow.GetComponent<Rigidbody>();
        var forceOffset = _averageUpwardForce * 0.2f;
        var randomUpwardForce = Random.Range(_averageUpwardForce - forceOffset, _averageUpwardForce + forceOffset);
        cowRigidbody.AddForce(new Vector3(Random.Range(-0.5f, 0.5f), 1, Random.Range(-0.5f, 0.5f)).normalized * randomUpwardForce);
        cowRigidbody.AddTorque(new Vector3(
            Random.Range(0, 360),
            Random.Range(0, 360),
            Random.Range(0, 360)
            ));

        var cowMultiplier = cow.GetComponent<CowMultiplier>();  
        cowMultiplier.Iterations = --Iterations;
        cowMultiplier.CowPrefab = CowPrefab;
    }
}

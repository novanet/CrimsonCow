using System.Collections;
using UnityEngine;

public class CowMultiplier : MonoBehaviour
{
    public int MaxIterations = 10;
    public int CurrentIteration = 0;
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
        
        var randomIndex = Random.Range(0, _audioSources.Length); 
        var audioSource = _audioSources[randomIndex];
        audioSource.pitch = Random.Range(0.5f, 1.5f);
        audioSource.Play();
    }

    public void OnCollisionEnter(Collision other)
    {
        if (enabled && CanCreate())
        {
            StartCoroutine(CreateNewCow(0f));
            StartCoroutine(CreateNewCow(0.2f));

            _notDuplicatedYet = false;
        }
    }

    private bool CanCreate()
    {
        var canCreate = Time.time > _creationTime + _minTimeBeforeDuplication
                        && _notDuplicatedYet
                        && CurrentIteration <= MaxIterations; 
        
        return canCreate;
    }

    private IEnumerator CreateNewCow(float delay)
    {
        var position = transform.position;
        yield return new WaitForSeconds(delay);
        
        // create larger cows for every iteration
        var scale = 1 + CurrentIteration / 3f;
        var cow = Instantiate(CowPrefab, position + new Vector3(0, 2 + scale, 0), Quaternion.identity, transform.parent);
        cow.transform.localScale = new Vector3(scale, scale, scale);

        // launch the cow upwards-ish
        var cowRigidbody = cow.GetComponent<Rigidbody>();
        var forceOffset = _averageUpwardForce * 0.2f;
        var randomUpwardForce = Random.Range(_averageUpwardForce - forceOffset, _averageUpwardForce + forceOffset);
        cowRigidbody.AddForce(new Vector3(Random.Range(-0.5f, 0.5f), 1, Random.Range(-0.5f, 0.5f)).normalized * randomUpwardForce);
        
        // give the cow a random spin
        cowRigidbody.AddTorque(new Vector3(
            Random.Range(0, 360),
            Random.Range(0, 360),
            Random.Range(0, 360)
            ));

        // initialize new cow multiplier, do not stack overflow
        var cowMultiplier = cow.GetComponent<CowMultiplier>();  
        cowMultiplier.CurrentIteration = ++CurrentIteration;
        cowMultiplier.CowPrefab = CowPrefab;
    }
}

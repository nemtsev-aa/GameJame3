using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    
    public float Radius;
    public float Force;
    public GameObject AffectArea;
    public ParticleSystem ParticleSystem;

    public void Explode()
    {
        StartCoroutine(AffectProcess());
    }

    private IEnumerator AffectProcess()
    {
        AffectArea.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        
        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, Radius);

        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            Rigidbody rigidbody = overlappedColliders[i].attachedRigidbody;
            if (rigidbody)
            {
                rigidbody.AddExplosionForce(Force, transform.position, Radius);
            }
        }

        //Destroy(gameObject);
        Instantiate(ParticleSystem, transform.position, Quaternion.identity);
    }

    private void OnValidate()
    {
        AffectArea.transform.localScale = Vector3.one * Radius * 2f;    
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public float Radius;
    public float Force;
    public GameObject AffectArea;
    public ParticleSystem ParticleSystem;
    public Animator Animator;

    public void Explode()
    {
        StartCoroutine(AffectProcess());
    }

    private IEnumerator AffectProcess()
    {
        //AffectArea.SetActive(true);

        Animator.SetBool("Affect", true);
        yield return new WaitForSeconds(1f);

        Collider[] overlappedColliders = Physics.OverlapSphere(transform.position, Radius);
        for (int i = 0; i < overlappedColliders.Length; i++)
        {
            Rigidbody rigidbody = overlappedColliders[i].attachedRigidbody;
            if (rigidbody)
            {
                rigidbody.AddExplosionForce(Force, transform.position, Radius);
            }
        }
                
        Instantiate(ParticleSystem, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnValidate()
    {
        AffectArea.transform.localScale = Vector3.one * Radius * 2f;    
    }
}

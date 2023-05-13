using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conscience : MonoBehaviour
{
    public void ShowParticle(ParticleSystem emotionEffect)
    {
        // Создаём эффект смери
        ParticleSystem dieParticle = Instantiate(emotionEffect, transform.position - Vector3.down * 1.5f, transform.rotation);
        Destroy(dieParticle, 0.5f);
    }
}

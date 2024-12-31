using UnityEngine;

public class ParticleHeightLimiter : MonoBehaviour
{
    public new ParticleSystem particleSystem; // Utilisez "new" pour masquer l'héritage
    public float maxHeight;

    private ParticleSystem.Particle[] particles;

    void Update()
    {
        if (particleSystem == null) return;

        if (particles == null || particles.Length < particleSystem.main.maxParticles)
        {
            particles = new ParticleSystem.Particle[particleSystem.main.maxParticles];
        }

        int count = particleSystem.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            if (particles[i].position.z > maxHeight)
            {
                particles[i].remainingLifetime = 0;
            }
        }

        particleSystem.SetParticles(particles, count);
    }
}

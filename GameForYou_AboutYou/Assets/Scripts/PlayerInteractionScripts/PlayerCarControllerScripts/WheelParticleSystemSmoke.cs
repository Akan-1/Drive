using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelParticleSystemSmoke : MonoBehaviour
{
	float particleEmissionRate = 0;

	CarMove carMove;

	ParticleSystem particleSystemSmoke;
	ParticleSystem.EmissionModule particleSystemEmissionModule;

	void Awake()
	{
		carMove = GetComponentInParent<CarMove>();

		particleSystemSmoke = GetComponent<ParticleSystem>();

		particleSystemEmissionModule = particleSystemSmoke.emission;

		particleSystemEmissionModule.rateOverTime = 0;
	}

	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		particleEmissionRate = Mathf.Lerp(particleEmissionRate, 0, Time.deltaTime * 5);
		particleSystemEmissionModule.rateOverTime = particleEmissionRate;

		if (carMove.IsTireScreeching(out float lateralVelocity, out bool isBraking))
		{
			if (isBraking)
				particleEmissionRate = 30;
			else particleEmissionRate = Mathf.Abs(lateralVelocity) * 2;
		}

	}
}

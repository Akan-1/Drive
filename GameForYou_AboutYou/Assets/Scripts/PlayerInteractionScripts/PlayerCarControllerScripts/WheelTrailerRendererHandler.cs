using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTrailerRendererHandler : MonoBehaviour
{
	CarMove carMove;
	TrailRenderer trailRenderer;

	void Awake()
	{
		carMove = GetComponentInParent<CarMove>();
		trailRenderer = GetComponent<TrailRenderer>();

		trailRenderer.emitting = false;
	}

    void Update()
    {
		if (carMove.IsTireScreeching(out float lateralVelocity, out bool isBraking))
			trailRenderer.emitting = true;

		else trailRenderer.emitting = false;
    }
}

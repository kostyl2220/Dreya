using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour {

    private List<Light> m_lightSources;

    private void Initialize()
    {
        var lights = Resources.FindObjectsOfTypeAll(typeof(Light));
        m_lightSources = new List<Light>(lights.Length);
        foreach (var obj in lights)
        {
            m_lightSources.Add((Light)obj);
        }
    }

    private float CalculateIntensityMultiplier(float distance, float maxDistance, float radius, float maxRadius)
    {
        float distanceIntensity = 1 - Mathf.Pow(radius / maxRadius, 2.0f);
        float radiusIntensity = 1 - Mathf.Pow(radius / maxRadius, 2.0f);
        return distanceIntensity * radiusIntensity;
    }

    public float CalculateLightIntensity(GameObject obj)
    {
        float endIntensity = 0.0f;
        foreach(var light in m_lightSources)
        {
            if (!light.enabled)
            {
                continue;
            }

            if (Vector3.Distance(obj.transform.position, light.transform.position) > light.range)
            {
                continue;
            }

            RaycastHit hit;
            if (Physics.Linecast(transform.position, obj.transform.position, out hit, LayerMask.NameToLayer("Player")))
            {
                continue;
            }

            Vector3 direction = obj.transform.position - light.transform.position;
            float maxAngle = light.spotAngle / 2;
            float targetAngle = Vector3.Angle(light.transform.forward, direction);
            if (targetAngle > maxAngle)
            {
                continue;
            }

            float targetDistance = direction.magnitude;
            float frontDistance = targetDistance * Mathf.Cos(Mathf.Deg2Rad * targetAngle);
            float maxRadius = Mathf.Tan(Mathf.Deg2Rad * maxAngle); 
            float targetRadius = Mathf.Tan(Mathf.Deg2Rad * targetAngle);

            float addedIntensity = CalculateIntensityMultiplier(frontDistance, light.range, targetRadius, maxRadius) * light.intensity;
            endIntensity += Mathf.Pow(addedIntensity, 2);
        }

        return Mathf.Sqrt(endIntensity);
    }

	// Use this for initialization
	void Start ()
    {
        Initialize();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}

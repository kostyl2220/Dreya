using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour {

    private List<Light> m_spotLightSources;
    private List<Light> m_pointLightSources;

    private void Initialize()
    {
        var allLights = Resources.FindObjectsOfTypeAll<Light>();
        m_spotLightSources = new List<Light>(allLights.Length);
        m_pointLightSources = new List<Light>(allLights.Length);
        foreach(var light in allLights)
        {
            if (light.gameObject.layer != GameDefs.LIGHT_LAYER)
            {
                continue;
            }

            if (light.type == LightType.Spot)
            {
                m_spotLightSources.Add(light);
            }
            if (light.type == LightType.Point)
            {
                m_pointLightSources.Add(light);
            }
        }
    }

    private float CalculateIntensityMultiplier(float distance, float maxDistance, float radius, float maxRadius)
    {
        float distanceIntensity = 1 - Mathf.Pow(distance / maxDistance, 2.0f);
        float radiusIntensity = 1 - Mathf.Pow(radius / maxRadius, 2.0f);
        return distanceIntensity * radiusIntensity;
    }

    private float CalculateIntensityMultiplier(float distance, float maxDistance)
    {
        float distanceIntensity = 1 - Mathf.Pow(distance / maxDistance, 2.0f);
        return distanceIntensity;
    }


    public float CalculateLightIntensity(GameObject obj)
    {
        return Mathf.Sqrt(Mathf.Pow(CalculatePointLightIntensity(obj), 2) + Mathf.Pow(CalculateSpotLightIntensity(obj), 2));
    }

    private float CalculatePointLightIntensity(GameObject obj)
    {
        float endIntensity = 0.0f;
        foreach (var light in m_pointLightSources)
        {
            if (!light.enabled)
            {
                continue;
            }

            Vector3 direction = obj.transform.position - light.transform.position;
            float targetDistance = direction.magnitude;

            if (targetDistance > light.range)
            {
                continue;
            }

            RaycastHit hit;
            if (Physics.Linecast(light.transform.position, obj.transform.position, out hit, LayerMask.NameToLayer("Player")))
            {
                continue;
            }
        
            float addedIntensity = CalculateIntensityMultiplier(targetDistance, light.range) * light.intensity;
            endIntensity += Mathf.Pow(addedIntensity, 2);
        }

        return Mathf.Sqrt(endIntensity);
    }

    private float CalculateSpotLightIntensity(GameObject obj)
    {
        float endIntensity = 0.0f;
        foreach(var light in m_spotLightSources)
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
            if (Physics.Linecast(transform.position, obj.transform.position, out hit, GameDefs.PLAYER_LAYER))
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class LightsController : MonoBehaviour
    {
        public List<GameObject> WhiteLights;
        public List<GameObject> RedLights;

        public GameObject light;

        public float main_frequency;
        public float burst_frequency;
        public float Clock;

        void Awake() {
            Clock = 0.0f;
            main_frequency = 1.0f;
            burst_frequency = 10.0f;
        }

        void Update() {
            Clock += Time.deltaTime;

            // White Lights
            if (Mathf.Sin(2 * Mathf.PI * Clock * burst_frequency) > 0.0 && 
                Mathf.Sin(2 * Mathf.PI * Clock * main_frequency) > 0.0)
                foreach (GameObject go in WhiteLights)
                    go.GetComponent<Renderer>().enabled = true;
            else
                foreach (GameObject go in WhiteLights)
                    go.GetComponent<Renderer>().enabled = false;

            // Red Lights
            if (Mathf.Sin(2 * Mathf.PI * Clock * burst_frequency) > 0.0 && 
                Mathf.Sin(2 * Mathf.PI * Clock * main_frequency + Mathf.PI) > 0.0)
                foreach (GameObject go in RedLights)
                    go.GetComponent<Renderer>().enabled = true;
            else 
                foreach (GameObject go in RedLights)
                    go.GetComponent<Renderer>().enabled = false;
        }
    }
}

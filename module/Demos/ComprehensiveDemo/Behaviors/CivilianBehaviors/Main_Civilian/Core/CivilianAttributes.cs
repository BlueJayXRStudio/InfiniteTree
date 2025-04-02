using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class CivilianAttributes : Attributes
    {
        Material m_Instance;
        public Color c_Green;
        public Color c_Red;

        void Awake() {
            m_Instance = new Material(GetComponent<Renderer>().sharedMaterial);
            GetComponent<Renderer>().material = m_Instance;

            c_Green = new Color(0.01568627f, 1f, 0f, 1f);
            c_Red = new Color(1f, 0.2734211f, 0f, 1f);
        }

        public bool ForceWake = false;
        public void Update() {
            var newColor = Color.Lerp(c_Red, c_Green, gameObject.GetComponent<Attributes>().Health/100.0f);
            m_Instance.color = newColor;

            if (gameObject.GetComponent<Attributes>().Health >= 20)
                gameObject.GetComponent<Attributes>().Health -= 4.0f * Time.deltaTime;
        }
    }
}

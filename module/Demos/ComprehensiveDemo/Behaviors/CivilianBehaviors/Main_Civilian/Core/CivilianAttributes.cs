using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class CivilianAttributes : Attributes
    {
        public bool ForceWake = false;
        public void Update() {
            if (gameObject.GetComponent<Attributes>().Health >= 20)
                gameObject.GetComponent<Attributes>().Health -= 5 * Time.deltaTime;
        }
    }
}

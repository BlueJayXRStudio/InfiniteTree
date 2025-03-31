using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class CivilianAttributes : Attributes
    {
        public bool ForceWake = false;
        public void Update() {
            gameObject.GetComponent<Attributes>().Health -= 5 * Time.deltaTime;
        }
    }
}

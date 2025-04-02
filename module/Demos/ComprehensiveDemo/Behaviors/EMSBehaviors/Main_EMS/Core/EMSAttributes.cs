using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class EMSAttributes : Attributes
    {
        public LightsController lightsController;
        void Awake() {
            lightsController = GetComponentInChildren<LightsController>(true);
        }
    }
}

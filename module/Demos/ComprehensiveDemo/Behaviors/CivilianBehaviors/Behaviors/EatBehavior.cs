using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class EatBehavior : Sequence
    {
        private List<Behavior> ToPopulate = new();
        
        public EatBehavior(GameObject go) : base(null, go) {
            DriverObject = go;
            Actions.Enqueue(new CheckFood(go));
            Actions.Enqueue(new a_EatFood(go));
        }
    }
}

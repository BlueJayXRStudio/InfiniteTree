using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    public class GetFood : Sequence
    {
        public GetFood(GameObject go) : base(null, go) {
            DriverObject = go;
            Actions.Enqueue(new CheckFood(go));
            Actions.Enqueue(new EatFood(go));
        }
    }
}
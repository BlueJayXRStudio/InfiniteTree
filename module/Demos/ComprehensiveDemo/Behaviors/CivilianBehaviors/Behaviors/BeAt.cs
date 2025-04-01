using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    internal class BeAt : Selector
    {
        public BeAt(GameObject go, (int, int) pos) : base(null, go)
        {
            Actions.Enqueue(new CheckPos(go, pos));
            Actions.Enqueue(new MoveTo(go, pos));
        }
    }
}
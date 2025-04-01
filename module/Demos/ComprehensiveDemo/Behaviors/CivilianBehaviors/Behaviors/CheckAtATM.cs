using System.Collections.Generic;
using UnityEngine;

namespace InfiniteTree
{
    internal class CheckAtATM : Selector
    {
        public CheckAtATM(GameObject go) : base(null, go)
        {
            Actions.Enqueue(new CheckPos(go, ExperimentBlackboard.Instance.ATMPos));
            Actions.Enqueue(new MoveTo(go, ExperimentBlackboard.Instance.ATMPos));
        }
    }
}
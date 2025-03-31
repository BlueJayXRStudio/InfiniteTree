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
            Actions.Enqueue(new EatFood(go));
        }

        // We are doing this because health is less than certain amount 
        // (i.e. HP < 80).
        // Because EatBehavior is a subclass of Sequence, the abstract
        // layer of logic such as this should not belong in this class,
        // but since this is a root node invoked by the FSM 
        // (CivilianControlFlow), the alternative would be to keep this
        // logic in the FSM, which may look unappealing. Thus, we will
        // just keep it here until we find better architectural options.
        public override Status CheckSuccess()
        {
            if (DriverObject.GetComponent<Attributes>().Health >= 80)
                return Status.SUCCESS;

            return Status.RUNNING;
        }

    }
}

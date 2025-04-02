**Author:** Jaeyoung Oh  
**Affiliation:** BlueJayXRStudio (formerly BlueJayVRStudio)  
**Contact:** BlueJayVRStudio@gmail.com  

# Task Stack Machine, an "Infinite" Tree
Novel Behavior Tree and State Machine Generalization.

planned documentation along with video tutorials

## Glossary

- **[Task Stack Machine](./module/TaskStackMachine.cs)**: Task Stack Machine Runner/Driver implementation.
- **[Sequence Composite](./module/Composites/Sequence.cs)**: Sequence Composite implementation.
- **[Selector Composite](./module/Composites/Selector.cs)**: Selector Composite implementation.
- **[Parallel Composite](./module/Composites/Parallel.cs)**: Parallel Composite implementation.
- **[Comprehensive Demo](./module/Demos/ComprehensiveDemo/)**: A Unity Demo using Task Stack Machine.

formalization (in progress):

> Central focus of this framework is smart use of stack memory and Task status messages to generalize action planning.  

> Type of action planning:
> 1. Procedural: Behavior Tree, Hierarchical Task Network Planning (`HTN`), Goal Oriented Action Planning (`GOAP`), etc.
>> - Pros: Highly expressive.
>> - Cons: Hard to construct (Unreal Engine resolves this by using node editor) and requires deep domain knowledge for the specific system we're implementing.
> 2. Reactive: Finite State Machine (`FSM`), etc.
>> - Pros: Can create smart emergent behaviors. 
>> - Cons: Difficult to decouple sequential behaviors. Some emergent behaviors may not be desired, which forces extensive robustness testing.

> The aim of Task Stack Machine is to generalize the two types of task planning frameworks by making use of stack memory
and a global access to the stack for each Task  
> Requirements for Task Stack Machine:  
> 
> **Note**: we will use "behaviors" and "tasks" interchangeably.
> 1. A Task is either Running or Done.
>> - If Running, return RUNNING status.
>> - If Done, return SUCCESS or FAILURE status.
> 2. A Task must receive status message and, more importantly, yield status message.
> 3. A Task can access shared memory.
> 4. A Tree must have a collection of Tasks. This can take many different forms including Stacks, Lists, Queues, or Priority Queue. 
Here, we will demonstrate the power of Stack memory in achieving wide variety of standard behaviors including procedural and reactive behaviors.

> Benefits
> - Loose coupling of behaviors,
>> - Behaviors are highly reusable,
>> - Leads to easy recursive state serialization,
>> - Each behavior can have a policy for decision making:
>>> - Priority queue for easy task prioritization in sequence nodes,
>>> - Machine/Deep Learning policies such as those used for PPO (training is "expensive", but reusability of behavior makes reinforcement learning highly viable),
>>> - Behavior nodes can be used to build a decision tree/graph with which we can run Dijkstra's algorithm for arbitrary cost optimization (GOAP),
>> - Behaviors are fully interpretable and traceable through OOP,
>> - Behaviors can act as a BT node or an FSM state,
> - Main tree can drive a full suite of standard behavior tree composites and decorators in an extremely compact and reproducible form,
>> - Approximate lines of code in .Net/C#:
>>> - Behavior tree: 50 lines, 
>>> - Behavior/Task Interface: 10 lines, 
>>> - Sequence/Selector Composite: 35 lines,
>>> - Parallel Composite: 45 lines,
>>> - Inverter Decorator: 30 lines,
>>> - Repeat Decorator: 0 line (Task can be made self-referential!) [BlueJay TODO: This is the part that connects BT back to FSM. A BT with Repeater + Selector + Blackboard -> Can simulate FSM]
> Cons
> - Can be difficult to debug. Very recursive in nature.
> - Lots of small scripts, but that may be a good thing in terms of OO principles.

## Formal Definition of the Task Stack Machine

**M = <T, S, μ, Σ, δ_tree, δ_task, t₀>**

| Symbol     | Description                                                                  |
|------------|------------------------------------------------------------------------------|
| `T`        | Set of all Tasks `t` (analogous to states `Q`)                               |
| `S`        | Set of all possible stacks over `T` (`Stack<T>`)                             |
| `Γ`        | Shared unbounded memory space (e.g. tape or arbitrary structured memory)     |
| `Σ`        | Set of status messages (`RUNNING`, `SUCCESS`, `FAILURE`)                     |
| `δ_tree`   | Scheduler/Dispatcher function: `Σ × S → T × S`                               |
| `δ_task`   | Task step function: `T × S × Γ × Σ → S × Γ × Σ`                              |
| `t₀ ∈ T`   | Initial task                                                                 |

![δ_system evolution](docs/system_evolution.svg)

Terminal states only exist at the Task level (`SUCCESS` or `FAILURE`), but the system itself (`δ_system`) is designed to run continuously, like a scheduler or dispatcher, awaiting new tasks.

<img src="docs/TaskStackMachinePseudoCode.png" alt="Task Stack Machine Algorithm" width="600"/>

Since we are working with unbounded memory in the forms of task stack and blackboard, our Task Stack Machine is a Turing complete machine. However, the usefulness of this framework does not come from Turing completeness, but rather from the structural differences between Task Stack Machine and other conventional AI backends such as FSM and Behavior Trees. 

### Behavior Tree (BT)

The implicit purpose of a BT is to drive an autonomous agent to a program completion, be it SUCCESS or FAILURE. Sequence node, for instance, sequentially (through pre-order DFS) invokes its children, and decides to continue iterating as long as its children output SUCCESS. In turn, Sequence node returns its own message to its parent (i.e. invoker, caller, etc.), and its parent will decide next course of action based on that message. So on and so forth recursively. While there is indeed an element of state transition in BT, the actual focus and intent is on defining a set of tasks that depend on the SUCCESS and FAILURE among one another. Thus, based on the semantics, a BT's function signature would look like this:  

`Task × Γ → Task x Γ x Σ`  
ExecuteFully(Subtask, Current Blackboard) = (Caller, Modified Blackboard, Status Message)

There is a subtask to caller transition and vice-versa, but no explicit transitions among nodes at the same hierarchy. BT does not allow arbitrary leaf to leaf transition or even composite to composite transition if they are not themselves contained within a parent composite. BT, therefore, strictly enforces logical or meaningful relationships among tasks. Although the presence of unbounded memory in the form of a blackboard does exist in BT and allows the flexibility to create an FSM if desired, it is simply not baked into the design.

### Finite State Machine (FSM)

FSM in a true definition is a simple machine; its state transitions depend only on external input alphabet (basic) and do not produce distinct outputs (Moore). Conventionally, however, FSM uses unbounded memory, making any computation possible. So, while it allows an arbitrary task to task transitions, it unfortunately means less reusability and modularity [7], which is what BT tries to resolve.

### Task Stack Machine (AKA Infinite Tree, TSM, ...)

Our Task Stack Machine attempts to resolve the intents of BT and FSM as a unified framework. By using a stack as a collection of tasks, we can simulate recursive function calls at the level of autonomous tasks, which inherently incorporates temporal dimension. Each task can call an arbitrary subtask by first pushing itself onto the stack then finally pushing the subtask with the message of RUNNING. At the end of that subtask's routine, status message of SUCCESS or FAILURE will be returned to the original task. This mirrors BT mechanisms exactly, and this module presents working examples of standard BT composites and decorators using TSM. On the other hand, FSM state transitions can be simulated by pushing the next state onto the task as RUNNING without first pushing the current task back onto the stack. In both BT and FSM under TSM generalization, a RUNNING task simply pushes itself back onto the task with the message of RUNNING without pushing any other task.

### Operating System and CPU Architecture

> *Another register is the stack pointer, which points to the top of the current stack in memory. The stack contains one frame for each procedure that has been entered but not yet exited. A procedure’s stack frame holds those input parameters, local variables, and temporary variables that are not kept in registers. - Tanenbaum, Modern Operating Systems [5]*

At the core of the Operating Systems model, we have a kernel that manages processes that can create multiple threads. Each thread has its own call stacks onto which it can push frames of procedures, AKA functions. In a typical CPU architecture, a procedure can call a new procedure, but it must be encapsulated in a new frame along with a return address to its original caller. The design yields a powerful programming paradigm that allows us to carry out complex recursive computations. However, it is surprisingly limited in autonomous systems or robotic policy representations, because a function f() cannot call itself without creating a new frame on the stack.

[BlueJay TODO: Function call stack example]

Because procedures in OS models cannot refer back to its original frame in the next execution cycle, it cannot in theory model an FSM which requires that a state be able to return back to itself across a temporal dimension. Even BT and HTN, which have recursive call structure, require that leaf nodes be able to continue running until they are ready to give control back to the parent composite. Thus, without an additional level of abstraction over procedural programming, it is impossible to achieve the kind of mechanisms desired by all currently existing autonomous policy representations such as FSM and BT (it must be noted that existing implementations of FSM and BT are in fact more than just a procedural programming paradigm). Therefore, there is a strong indication that there exists an alternative minimal system structure to emulate an autonomous agent - possibly even mimicking self-awareness to slightly exaggerate.

[BlueJay TODO: TSM call stack example]

By encapsulating a function in an object structure, allowing free-form manipulation of the stack by the functions, and finally converting the responsibility of the call stack to that of keeping traces of object frames rather than procedure frames, we can achieve a fundamentally different type of computational paradigm that allows for the unification of various robotic policy representations. Additionally, flexible manipulation of the stack and clever use of OOP techniques allow each behavior or task to be able to gather hierarchical contextual information that could be used to create context aware adaptiveness and interruptibility in run time complexity that scales logarithmically with the number of tasks within a BT or HTN. That is practically in constant time.

For example, if we imagine a task such as "EatBehavior", we can quickly decompose this into a hierarchical structure. EatBehavior (sequence) requires that we check for food (selector) and then finally eat it (a primitive, non-decomposable task). Checking if we already have food requires that we look into our inventory and on failure to find food in the inventory, we must perform the action of getting food (sequence). An action of getting food requires that we first check if we have cash and then finally purchase food. Checking if we have cash involves checking our wallet, then on low cash we can perform the action of withdrawing cash. Purchasing food and withdrawing cash are, of course, decomposable actions themselves. Although we will skip the details of those actions for brevity, we need only know that those two will eventually reach the primitive tasks of actually moving towards an ATM or a grocery store, which are costly actions in terms of energy and time. Thus, it is in the agent's best interest to know when to interrupt their current task, yet this is one of the most notorious issues in autonomous systems.

[BlueJay TODO: Eat Behavior]

Although the entire problem domain of interruptibility has not been studied as a part of this report, we already have a working demonstration of the ease with which TSM can solve some subset of interruptibility problems. Going back to our EatBehavior tree, we can note that physically moving to a location is an atomic operation. That means that once we enter that primitive task, we cannot exit until it is completed. And, because it is a highly reused behavior, it is difficult to adapt this action without rewriting the code for a specific decomposable action or without writing a complicated set of conditional flows that use information from sensory devices and memory. With TSM, however, we can simply create an interface called ICheckTermination with the method CheckTermination(), which can be implemented by non-primitive tasks (such as our main EatBehavior) to return whether the task has already been fulfilled or failed. Then, for every costly atomic, primitive action, we can simply traverse the current stack memory and for each task within that stack, check whether it implements ICheckTermination, and if so, invoke CheckTermination to see if we should exit the atomic action.

There are three pathways in our EatBehavior tree where the agent needs to perform MoveTo action and would want to know when to terminate the task. 

1. Agent does not have enough food -> has enough cash -> Grocery Store
2. Agent does not have enough food -> does not have enough cash -> ATM

In condition 1, since the agent is moving towards the grocery store *because* it does not have enough food, it only needs to check the satisfaction requirement of having enough food.
Whereas in condition 2, since the agent is moving towards the ATM *because* it does not have enough cash *because* it does not have enough food, it must check satisfaction requirement of having enough food and enough cash. If we only check whether we have enough food, that means when we suddenly receive cash on the way to the ATM, we will fail to exit out of that atomic operation. This is profound in a philosophical sense, because even we, as a human, easily forget why we were doing certain chain of actions, and often end up wasting considerable amount of time that could have been saved if we were aware of the context of our actions.  

Instead of programming the MoveTo behavior to interrupt itself when the agent suddenly receives food or cash, we can simply implement ICheckTermination on EatBehavior and GetCashBehavior which would return SUCCESSFUL if the agent already has enough food or cash, respectively. In each frame while it is RUNNING, MoveTo simply has to traverse the current memory to check if any of the requirements has already been met by invoking CheckTermination(), and can exit succesfully if that were the case. There is no need to script a separate MoveTo behavior to handle for each and every case nor the need to use arbitrary shared memory to keep track of requirements which goes against OO principles. All requirements are well encapsulated in each behavior, so anytime we need to reuse GetCashBehavior for different occasions such as going to the movie theater or a shopping mall, MoveTo simply needs to *remember* why it was doing that operation which is defined strictly in the behaviors that are currently in the stack memory. And to optimize this even further, we could separate out the logic of checking the memory into a behavioral strategy pattern, thus adhering to best practices in terms of OOAD.

### TSM Sequence Composite Design

![δ_system evolution](docs/SequenceComposite.png)  
![δ_system evolution](docs/SequenceCompositeNonRoot.png)  

[BlueJay TODO: Sequence Algorithm]  

Sequence in Natural Language: "If *I* have previously failed, then *I* will stop the sequence. Otherwise, if *I* have run out of tasks, then *I* have succeeded. Otherwise *I* will continue with the next task."

<img src="docs/SequencePseudoCode.png" alt="Task Stack Machine Algorithm" width="600"/>

![δ_system evolution](docs/FSM.png)  
![δ_system evolution](docs/FlexFSM.png)  

[BlueJay TODO: FSM example pseudocode]

## References

1. Erol, K., Hendler, J. A., & Nau, D. S. (1994, June). UMCP: A sound and complete procedure for hierarchical task-network planning. In Aips (Vol. 94, pp. 249-254).
2. Orkin, J. (2006, March). Three states and a plan: the AI of FEAR. In Game developers conference (Vol. 2006, p. 4). SanJose, California: CMP Game Group.
3. Rajeev Alur, Michael Benedikt, Kousha Etessami, Patrice Godefroid, Thomas Reps, and Mihalis Yannakakis. 2005. Analysis of recursive state machines. ACM Trans. Program. Lang. Syst. 27, 4 (July 2005), 786–818. https://doi.org/10.1145/1075382.1075387
4. John E. Hopcroft, Rajeev Motwani, and Jeffrey D. Ullman. 2001. Introduction to automata theory, languages, and computation, 2nd edition. SIGACT News 32, 1 (March 2001), 60–65. https://doi.org/10.1145/568438.568455
5. Andrew S. Tanenbaum and Herbert Bos. 2014. Modern Operating Systems (4th. ed.). Prentice Hall Press, USA.
6. Clarke, E. M., Grumberg, O., & Peled, D. A. (1999). Model checking. MIT Press.
7. Turing, A.M. (1937), On Computable Numbers, with an Application to the Entscheidungsproblem. Proceedings of the London Mathematical Society, s2-42: 230-265. https://doi.org/10.1112/plms/s2-42.1.230
8. Iovino, M., Förster, J., Falco, P., Chung, J. J., Siegwart, R., & Smith, C. (2024). Comparison between Behavior Trees and Finite State Machines. arXiv preprint arXiv:2405.16137.
9. Epic Games, *Behavior Trees in Unreal Engine*, Unreal Engine Documentation, [Online]. Available: https://dev.epicgames.com/documentation/en-us/unreal-engine/behavior-trees-in-unreal-engine. [Accessed: Mar. 27, 2025].
10. Splintered Reality, *py_trees*, GitHub repository, [Online]. Available: https://github.com/splintered-reality/py_trees
11. EugenyN, *BehaviorTrees*, GitHub repository, [Online]. Available: https://github.com/EugenyN/BehaviorTrees
12. Eraclys, *BehaviourTree*, GitHub repository, [Online]. Available: https://github.com/Eraclys/BehaviourTree
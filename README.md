**Author:** Jaeyoung Oh  
**Affiliation:** BlueJayXRStudio (formerly BlueJayVRStudio)  
**Contact:** BlueJayVRStudio@gmail.com  

# Task Stack Machine, an "Infinite" Tree
Novel Behavior Tree and State Machine Generalization.

planned documentation along with video tutorials

## Glossary

- **[Behavior Tree](./module/BehaviorTree.cs)**: Main Behavior Tree Runner/Driver.

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
>> - Behaviors are fully interpretable,
>> - Behaviors can act as a Turing complete behavior tree node or a state in a finite state machine,
> - Main tree can drive a full suite of standard behavior tree composites and decorators in an extremely compact and reproducible form,
>> - Approximate lines of code in .Net/C#:
>>> - Behavior tree: 50 lines, 
>>> - Behavior/Task Interface: 10 lines, 
>>> - Sequence/Selector Composite: 35 lines,
>>> - Parallel Composite: 45 lines,
>>> - Inverter Decorator: 30 lines,
>>> - Repeater Decorator: 0 line (Task can be made self-referential!)
> Cons
> - Hard to debug. Very recursive in nature.

## Formal Definition of the Task Stack Machine

**M = <T, S, μ, Σ, δ_tree, δ_task, t₀>**

| Symbol     | Description                                                                  |
|------------|------------------------------------------------------------------------------|
| `T`        | Set of all Tasks (analogous to states `Q`)                                   |
| `S`        | Set of all stacks over `T` (`Stack<T>`)                                      |
| `μ`        | Shared memory space (e.g., map, tape, or arbitrary structured memory)        |
| `Σ`        | Set of status messages (`RUNNING`, `SUCCESS`, `FAIL`)                        |
| `δ_tree`   | Scheduler/Dispatcher function: `Σ × S → T × Σ × S`                           |
| `δ_task`   | Task step function: `T × S × μ × Σ → T × S × μ × Σ`                          |
| `t₀ ∈ T`   | Initial task                                                                 |

![δ_system evolution](docs/system_evolution.svg)

The Task Stack Machine does not halt. Terminal states only exist at the Task level (`SUCCESS` or `FAIL`), but the system itself (`δ_system`) is designed to run continuously, like a scheduler or CPU, awaiting new tasks.

## References

1. Erol, K., Hendler, J. A., & Nau, D. S. (1994, June). UMCP: A sound and complete procedure for hierarchical task-network planning. In Aips (Vol. 94, pp. 249-254).
2. Orkin, J. (2006, March). Three states and a plan: the AI of FEAR. In Game developers conference (Vol. 2006, p. 4). SanJose, California: CMP Game Group.
3. Rajeev Alur, Michael Benedikt, Kousha Etessami, Patrice Godefroid, Thomas Reps, and Mihalis Yannakakis. 2005. Analysis of recursive state machines. ACM Trans. Program. Lang. Syst. 27, 4 (July 2005), 786–818. https://doi.org/10.1145/1075382.1075387
4. John E. Hopcroft, Rajeev Motwani, and Jeffrey D. Ullman. 2001. Introduction to automata theory, languages, and computation, 2nd edition. SIGACT News 32, 1 (March 2001), 60–65. https://doi.org/10.1145/568438.568455
5. Clarke, E. M., Grumberg, O., & Peled, D. A. (1999). Model checking. MIT Press.
6. Turing, A.M. (1937), On Computable Numbers, with an Application to the Entscheidungsproblem. Proceedings of the London Mathematical Society, s2-42: 230-265. https://doi.org/10.1112/plms/s2-42.1.230
7. Epic Games, *Behavior Trees in Unreal Engine*, Unreal Engine Documentation, [Online]. Available: https://dev.epicgames.com/documentation/en-us/unreal-engine/behavior-trees-in-unreal-engine. [Accessed: Mar. 27, 2025].
8. Splintered Reality, *py_trees*, GitHub repository, [Online]. Available: https://github.com/splintered-reality/py_trees
9. EugenyN, *BehaviorTrees*, GitHub repository, [Online]. Available: https://github.com/EugenyN/BehaviorTrees
10. Eraclys, *BehaviourTree*, GitHub repository, [Online]. Available: https://github.com/Eraclys/BehaviourTree
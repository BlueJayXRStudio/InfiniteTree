**Author:** Jaeyoung Oh  
**Affiliation:** BlueJayXRStudio (formerly BlueJayVRStudio)  
**Contact:** BlueJayVRStudio@gmail.com  

# Task Stack Machine, an "Infinite" Tree
Novel Behavior Tree and State Machine Generalization.

planned documentation along with video tutorials

formalization (in progress):

> Central focus of this framework is the smart use of stack memory and Task status messages.
> - Task = Behavior
> - Task is either Running or Done.
>> - If Running, return RUNNING status.
>> - If Done, return SUCCESS or FAILURE status.
> - A Task must yield status message and also have ways to receive status message, but it is up to them to use it or not.
> - A Task can access shared memory.
> - A Tree must have Task memory. This can take many different forms including Stacks, Lists, Queues, or Priority Queue. Here, we will focus on the use Stacks to achieve wide variety of standard behaviors.

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


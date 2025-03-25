# Task Stack Machine, an "Infinite" Tree
Novel Behavior Tree and State Machine Generalization.

planned documentation along with video tutorials

formalization (in progress):

> Central focus of this framework is the smart use of stack memory and Task status messages.

> Task = Behavior

> - Task is either Running or Done.
>> - If Running, return RUNNING status.
>> - If Done, return SUCCESS or FAILURE status.
> - A Task must yield status message and also have ways to receive status message, but it is up to them to use it or not.
> - A Task can access shared memory.
> - A Tree must have Task memory. This can take many different forms including Stacks, Lists, Queues, or Priority Queue. Here, we will focus on the use Stacks to achieve wide variety of standard behaviors.


## Formal Definition of the Task Stack Machine

**M = <T, S, μ, Σ, δ_tree, δ_task, t₀>**

| Symbol     | Description                                                                  |
|------------|------------------------------------------------------------------------------|
| `T`        | Set of all Tasks (analogous to states `Q`)                                   |
| `S`        | Set of all stacks over `T` (`Stack<T>`)                                      |
| `μ`        | Shared memory space (e.g., map, tape, or arbitrary structured memory)        |
| `Σ`        | Set of status messages (`RUNNING`, `SUCCESS`, `FAIL`)                        |
| `δ_tree`   | Dispatcher function: `Σ × S → T × Σ × S`                                     |
| `δ_task`   | Task step function: `T × S × μ × Σ → T × S × μ × Σ`                          |
| `t₀ ∈ T`   | Initial task                                                                 |

```
System Evolution:

δ_system(t, s, μ, σ) = 
    let (t', σ', s') = δ_tree(σ, s) 
    in δ_task(t', s', μ, σ')

δ_system: T × S × μ × Σ → T × S × μ × Σ
```

![δ_system evolution](docs/system_evolution.svg)
# Task Stack Machine, an "Infinite" Tree
(Potentially) Novel Behavior Tree and State Machine Generalization.

planned documentation along with video tutorials

formalization (in progress):

> Central focus of this framework is the smart use of stack memory and Task status messages.

> Task = Behavior

> - Task is either Running or Done.
>> - If Running, return RUNNING status.
>> - If Done, return SUCCESS or FAILURE status.
> - A Task must yield status message.
> - A Task must receive status message.
> - A Task can access shared memory.

> - A Task has
> - A Tree has a stack memory


## Formal Definition of the Task Stack Machine

**M = (T, S, M, Σ, δ_tree, δ_task, t₀)**

| Symbol     | Description                                                                 |
|------------|-----------------------------------------------------------------------------|
| `T`        | Set of all Tasks (analogous to states `Q`)                                  |
| `S`        | Set of all stacks over `T` (`Stack<T>`)                                     |
| `M`        | Shared memory space (e.g., map, tape, or arbitrary structured memory)       |
| `Σ`        | Set of status messages (`RUNNING`, `SUCCESS`, `FAIL`, etc.)                |
| `δ_tree`   | Dispatcher function: `Σ × S → T × Σ × S`                                     |
| `δ_task`   | Task step function: `T × S × M × Σ → T × S × M × Σ`                          |
| `t₀ ∈ T`   | Initial task                                                                |
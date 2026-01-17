# Variations

## 1) Linear happy path
Description: Validates single-thread execution, trace creation, and End mapping with no branching or suspension.
Workflow: `Start -> Edit -> End`

## 2) FanOut/FanIn basic
Description: Validates split and join semantics, plus output merge from parallel branches into the parent thread context.
Workflow: `Start -> FanOut(2) -> { A: Code, B: Code } -> FanIn -> End`

## 3) FanOut/FanIn mismatch
Description: Ensures the engine detects branches that converge on different FanIn nodes and fails the run.
Workflow: `Start -> FanOut(2) -> { A: FanIn-1, B: FanIn-2 }`

## 4) FanOut with uneven duration
Description: Validates that FanIn waits for the slowest branch regardless of arrival order or timing.
Workflow: `Start -> FanOut(3) -> { A: Code(delay), B: Code(delay), C: Code(delay) } -> FanIn -> End`

## 5) Batch basic
Description: Validates slicing, parallelism limits, and the single continue path after all batch work completes.
Workflow: `Start -> Batch(list 25, size 10, parallel 2) -> { process: Process, continue: End }`

## 6) Batch size zero
Description: Confirms that batch size zero produces a single batch that contains the full list.
Workflow: `Start -> Batch(list 5, size 0) -> { process: Process, continue: End }`

## 7) Batch empty list
Description: Confirms that the process branch is skipped when the input list is empty and continue runs once.
Workflow: `Start -> Batch(list 0) -> { process: Process, continue: End }`

## 8) Batch output merge
Description: Validates that per-batch outputs are merged into the parent context and visible on the continue path.
Workflow: `Start -> Batch -> { process: Process(writes output), continue: End(reads output) }`

## 9) FanOut with batch per branch
Description: Validates nested grouping: each branch runs its own batch set, then FanIn joins and merges outputs.
Workflow: `Start -> FanOut(2) -> { A: Batch -> FanIn, B: Batch -> FanIn } -> End`

## 10) Gosub basic
Description: Validates subworkflow input mapping, child run completion, and output mapping back into the parent context.
Workflow: `Start -> WorkflowCall(subworkflow) -> End`
Subworkflow: `Start -> Edit -> End`

## 11) Gosub inside fanout
Description: Validates that a gosub can run in one branch while another branch continues, and FanIn joins only after the child completes.
Workflow: `Start -> FanOut(2) -> { A: WorkflowCall, B: Edit } -> FanIn -> End`
Subworkflow: `Start -> Code -> End`

## 12) UserInput simple
Description: Validates suspension, persistence readiness, and resume on user reply for a single thread.
Workflow: `Start -> UserInput -> End`

## 13) UserInput with other runnable threads
Description: Validates that a waiting user input does not suspend the whole run while other branches are still runnable.
Workflow: `Start -> FanOut(2) -> { A: UserInput, B: Code } -> FanIn -> End`

## 14) Multiple UserInput queue
Description: Validates serialized prompts: only one active wait, others queued; run persists when all are waiting and resumes in order.
Workflow: `Start -> FanOut(2) -> { A: UserInput, B: UserInput } -> FanIn -> End`

## 15) UserInput + Batch + Gosub
Description: Validates that batch scheduling, gosub completion, and user input serialization all coexist without blocking worker slots.
Workflow: `Start -> Batch -> Process(WorkflowCall) -> UserInput -> End`
Subworkflow: `Start -> FanOut(2) -> { A: Code, B: Edit } -> FanIn -> End`

## 16) UserInput while gosub suspended
Description: Validates queuing across parent and child waits: only one active prompt, others queued; resume in order after rehydration.
Workflow: `Start -> FanOut(2) -> { A: WorkflowCall(waiting), B: UserInput } -> FanIn -> End`
Subworkflow: `Start -> UserInput -> End`

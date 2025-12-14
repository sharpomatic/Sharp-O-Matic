import { RunStatus } from "../../../enumerations/run-status";

export interface RunProgressModel {
  workflowId: string;
  runId: string;
  inputEntries?: string;
  outputContext?: string;
  runStatus: RunStatus;
  message: string;
  error: string;
}

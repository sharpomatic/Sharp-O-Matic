import { NodeStatus } from "../../../enumerations/node-status";

export interface TraceProgressModel {
  workflowId: string;
  runId: string;
  traceId: string;
  nodeEntityId: string;
  nodeStatus: NodeStatus;
  inputContext?: string;
  outputContext?: string; 
  message: string;
  error: string;
  title: string;
  symbol: string;
}


import { computed, signal, WritableSignal } from '@angular/core';
import { ConnectorEntity } from './connector.entity';
import { NodeEntity, NodeSnapshot } from './node.entity';
import { NodeType } from '../enumerations/node-type';

export interface GosubNodeSnapshot extends NodeSnapshot {
  workflowId: string | null;
}

export class GosubNodeEntity extends NodeEntity<GosubNodeSnapshot> {
  public workflowId: WritableSignal<string | null>;

  constructor(snapshot: GosubNodeSnapshot) {
    super(snapshot);

    this.workflowId = signal(snapshot.workflowId ?? null);

    const baseIsDirty = this.isDirty;
    this.isDirty = computed(() => {
      const snapshot = this.snapshot();
      const currentIsDirty = baseIsDirty();
      const currentWorkflowId = this.workflowId();

      return currentIsDirty ||
        currentWorkflowId !== snapshot.workflowId;
    });
  }

  public override toSnapshot(): GosubNodeSnapshot {
    return {
      ...super.toNodeSnapshot(),
      workflowId: this.workflowId(),
    };
  }

  public static fromSnapshot(snapshot: GosubNodeSnapshot): GosubNodeEntity {
    return new GosubNodeEntity(snapshot);
  }

  public static override defaultSnapshot(): GosubNodeSnapshot {
    return {
      ...NodeEntity.defaultSnapshot(),
      nodeType: NodeType.Gosub,
      title: 'Gosub',
      inputs: [ConnectorEntity.defaultSnapshot()],
      outputs: [ConnectorEntity.defaultSnapshot()],
      workflowId: null,
    };
  }

  public static create(top: number, left: number): GosubNodeEntity {
    return new GosubNodeEntity({
      ...GosubNodeEntity.defaultSnapshot(),
      top,
      left,
    });
  }
}

import { Component, EventEmitter, Inject, OnInit, Output, TemplateRef, ViewChild, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { DIALOG_DATA } from '../services/dialog.service';
import { TabComponent, TabItem } from '../../components/tab/tab.component';
import { ContextViewerComponent } from '../../components/context-viewer/context-viewer.component';
import { TraceProgressModel } from '../../pages/workflow/interfaces/trace-progress-model';
import { GosubNodeEntity } from '../../entities/definitions/gosub-node.entity';
import { ServerRepositoryService } from '../../services/server.repository.service';
import { WorkflowSummaryEntity } from '../../entities/definitions/workflow.summary.entity';

@Component({
  selector: 'app-gosub-node-dialog',
  imports: [
    CommonModule,
    FormsModule,
    TabComponent,
    ContextViewerComponent,
  ],
  templateUrl: './gosub-node-dialog.component.html',
  styleUrls: ['./gosub-node-dialog.component.scss'],
})
export class GosubNodeDialogComponent implements OnInit {
  private readonly serverRepository = inject(ServerRepositoryService);

  @Output() close = new EventEmitter<void>();
  @ViewChild('detailsTab', { static: true }) detailsTab!: TemplateRef<unknown>;
  @ViewChild('inputsTab', { static: true }) inputsTab!: TemplateRef<unknown>;
  @ViewChild('outputsTab', { static: true }) outputsTab!: TemplateRef<unknown>;

  public node: GosubNodeEntity;
  public inputTraces: string[];
  public outputTraces: string[];
  public workflows: WorkflowSummaryEntity[] = [];
  public tabs: TabItem[] = [];
  public activeTabId = 'details';

  constructor(@Inject(DIALOG_DATA) data: { node: GosubNodeEntity, nodeTraces: TraceProgressModel[] }) {
    this.node = data.node;
    this.inputTraces = (data.nodeTraces ?? []).map(trace => trace.inputContext).filter((context): context is string => context != null);
    this.outputTraces = (data.nodeTraces ?? []).map(trace => trace.outputContext).filter((context): context is string => context != null);
  }

  ngOnInit(): void {
    this.tabs = [
      { id: 'details', title: 'Details', content: this.detailsTab },
      { id: 'inputs', title: 'Inputs', content: this.inputsTab },
      { id: 'outputs', title: 'Outputs', content: this.outputsTab },
    ];

    this.serverRepository.getWorkflowSummaries('', 0, 0).subscribe(workflows => {
      this.workflows = workflows;
    });
  }

  onClose(): void {
    this.close.emit();
  }

  onWorkflowIdChange(value: string | null): void {
    const trimmed = value?.trim() ?? '';
    this.node.workflowId.set(trimmed.length > 0 ? trimmed : null);
  }
}

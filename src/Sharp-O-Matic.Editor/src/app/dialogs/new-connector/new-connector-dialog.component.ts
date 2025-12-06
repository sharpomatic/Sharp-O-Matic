import { CommonModule } from '@angular/common';
import { Component, effect, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ConnectionConfig } from '../../metadata/definitions/connection-config';
import { MetadataService } from '../../services/metadata.service';

@Component({
  selector: 'app-newconnector-dialog',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './new-connector-dialog.component.html',
  styleUrls: ['./new-connector-dialog.component.scss'],
})
export class SelectConnectorDialogComponent {
  private readonly metadataService = inject(MetadataService);

  public readonly connectionConfigs = this.metadataService.connectionConfigs;
  public selectedConfigId: string | null = null;
  public resultConfigId: string | null = null;

  constructor(public readonly bsModalRef: BsModalRef<SelectConnectorDialogComponent>) {
    effect(() => {
      const configs = this.connectionConfigs();

      if (!configs.length) {
        this.selectedConfigId = null;
        return;
      }

      if (!this.selectedConfigId || !configs.some(config => config.configId === this.selectedConfigId)) {
        this.selectedConfigId = configs[0].configId;
      }
    });
  }

  public get selectedConfigDescription(): string | undefined {
    const match = this.connectionConfigs().find(config => config.configId === this.selectedConfigId);
    return match?.description;
  }

  public trackByConfigId(_: number, config: ConnectionConfig): string {
    return config.configId;
  }

  public confirm(): void {
    if (!this.selectedConfigId) {
      return;
    }

    this.resultConfigId = this.selectedConfigId;
    this.bsModalRef.hide();
  }

  public cancel(): void {
    this.resultConfigId = null;
    this.bsModalRef.hide();
  }
}

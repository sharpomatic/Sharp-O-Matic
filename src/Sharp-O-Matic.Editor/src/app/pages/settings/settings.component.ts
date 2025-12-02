import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SettingsService } from '../../services/settings.service';

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent {
  private readonly settingsService = inject(SettingsService);
  apiUrlValue = this.settingsService.apiUrl();

  onApiUrlChange(value: string): void {
    this.settingsService.setApiUrl(value);
    this.apiUrlValue = this.settingsService.apiUrl();
  }
}

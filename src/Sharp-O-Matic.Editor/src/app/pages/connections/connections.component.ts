import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsModalService } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';

@Component({
  selector: 'app-connections',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './connections.component.html',
  styleUrls: ['./connections.component.scss'],
    providers: [BsModalService]
})
export class ConnectionsComponent {
  private readonly modalService = inject(BsModalService);
  private readonly router = inject(Router);  
}

import { ChangeDetectionStrategy, Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'stroytorg-domain',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './domain.component.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class DomainComponent {}

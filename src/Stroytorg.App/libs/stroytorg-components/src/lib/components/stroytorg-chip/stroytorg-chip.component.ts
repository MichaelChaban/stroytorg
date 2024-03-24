import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { ChipPalette } from './stroytorg-chip.models';
import { StroytorgRippleDirective } from '@stroytorg/shared';

@Component({
  selector: 'stroytorg-chip',
  templateUrl: './stroytorg-chip.component.html',
  standalone: true,
  imports: [CommonModule, MatIconModule, StroytorgRippleDirective],
})
export class StroytorgChipComponent {
  @Input()
  title = '';

  @Input()
  disabled = false;

  @Input()
  removable = false;

  @Input()
  selectable = false;

  @Input()
  selected = false;

  @Input()
  palette: ChipPalette = 'default-chip';

  @Output()
  removeChanges = new EventEmitter<string>();

  @Output()
  selectChanges = new EventEmitter<string>();

  removeChip($event: MouseEvent) {
    $event.stopPropagation();
    this.removeChanges.emit(this.title);
  }

  selectChip() {
    if (this.selectable) {
      this.selected = !this.selected;
      this.selectChanges.emit(this.title);
    }
  }
}

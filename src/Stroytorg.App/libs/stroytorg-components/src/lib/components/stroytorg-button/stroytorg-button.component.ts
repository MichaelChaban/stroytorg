import {
  ChangeDetectionStrategy,
  Component, EventEmitter, HostListener,
  Input, Output,
  ViewEncapsulation,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import { ButtonSize, ButtonType, CustomIcon } from './stroytorg-buttons.models';
// import { APP_CONFIG_ASSETS_BASE_URL } from "@stroytorg/shared";

@Component({
  selector: 'stroytorg-button',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './stroytorg-button.component.html',
  styleUrls: ['./stroytorg-button.component.scss'],
  changeDetection: ChangeDetectionStrategy.Default,
  encapsulation: ViewEncapsulation.None,
})
export class StroytorgButtonComponent {

  // assets = inject(APP_CONFIG_ASSETS_BASE_URL);
  @Input()
  size = ButtonSize.normal as string;

  @Input()
  disabled!: boolean;

  @Input()
  type = ButtonType.default as string;

  @Input()
  title!: string;

  @Input()
  icon!: CustomIcon;

  @Input() buttonType: 'submit' | 'button' = 'button' ;

  @Output() clicked = new EventEmitter<void>();
  
  @HostListener('click')
  clickedListen() {
    this.clicked.emit();
  }
}

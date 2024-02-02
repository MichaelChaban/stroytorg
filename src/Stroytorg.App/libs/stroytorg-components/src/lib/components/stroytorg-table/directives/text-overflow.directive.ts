/* eslint-disable @angular-eslint/directive-selector */
import { Directive, AfterViewInit, ElementRef, HostListener, AfterViewChecked, Input } from '@angular/core';
import { ShortcutService } from '../services/shortcut.service';

@Directive({
  selector: 'stroytorg-table[useOverflowShortcut]',
  exportAs: 'stroytorgTableTextOverflow',
  standalone: true,
})
export class TableOverflowShortcutDirective implements AfterViewInit, AfterViewChecked {
  private useShortcuts = false;
  
  constructor(
    private el: ElementRef,
    private shortcutService: ShortcutService
  ) {}

  @Input() scaleFactor = 0.8;

  ngAfterViewInit() {
    this.updateShortcutDisplay();
  }

  ngAfterViewChecked() {
    this.updateShortcutDisplay();
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: Event) {
    this.updateShortcutDisplay();
  }

  private updateShortcutDisplay() {
    const table = this.el.nativeElement;
    const scrollBarWidth = 20; 
    const scaleFactor = this.scaleFactor;
      if (table.offsetWidth > (window.innerWidth - scrollBarWidth) * scaleFactor) {
        if (!this.useShortcuts) {
          this.useShortcuts = true;
          this.applyShortcuts();
        }
      } else {
        if (this.useShortcuts) {
          this.useShortcuts = false;
          this.restoreOriginalText();
        }
      }
  }

  private applyShortcuts() {
    const headers = Array.from(this.el.nativeElement.querySelectorAll('th'));
    const shortcuts = this.shortcutService.getShortcuts();

    headers.forEach((header: any) => {
      const span = header.querySelector('span');
      if (span) {
        const headerText = span.innerText;
        const matchingShortcut = shortcuts.find(
          (record) => record['original'] === headerText.toLowerCase()
        );

        if (matchingShortcut) {
          const shortcut = matchingShortcut['shortcut'];
          span.innerHTML = shortcut;
          span.title = headerText;
        }
      }
    });
  }

  private restoreOriginalText() {
    const headers = Array.from(this.el.nativeElement.querySelectorAll('th'));
    const shortcuts = this.shortcutService.getShortcuts();

    headers.forEach((header: any) => {
      const span = header.querySelector('span');
      if (span) {
        const shortcut = span.innerHTML;
        const matchingOriginal = shortcuts.find(
          (record) => record['shortcut'] === shortcut
        );

        if (matchingOriginal) {
          const original = matchingOriginal['original'];
          span.innerHTML = original;
          span.title = '';
        }
      }
    });
  }
}
/* eslint-disable @typescript-eslint/no-explicit-any */
import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { NavbarModel } from './stroytorg-navbar.models';

@Component({
  selector: 'stroytorg-stroytorg-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './stroytorg-navbar.component.html',
  styleUrl: './stroytorg-navbar.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class StroytorgNavbarComponent implements OnInit {
  @Input()
  items!: NavbarModel[];

  ngOnInit(): void {
      if(!this.items){
        throw new Error('Items must be defined!');
      }
  }

  navigationChange(e: any) {
    const newPage = e.target.innerHTML;
    this.items.forEach(x => {
      x.active = x.name === newPage
    })
  }
}

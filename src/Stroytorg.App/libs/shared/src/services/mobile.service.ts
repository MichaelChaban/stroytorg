import { Injectable } from '@angular/core';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';

@Injectable({
  providedIn: 'root',
})
export class MobileService {
  
  private isMobile!: boolean;

  constructor(private breakpointObserver: BreakpointObserver) {
    this.breakpointObserver
      .observe([Breakpoints.Handset, Breakpoints.Tablet, Breakpoints.Small])
      .subscribe((result) => {
        this.isMobile = result.matches;
      });
  }

  getIsMobile(): boolean {
    return this.isMobile;
  }
}

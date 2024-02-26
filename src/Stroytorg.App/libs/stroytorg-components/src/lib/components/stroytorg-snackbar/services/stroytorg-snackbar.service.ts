import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { SnackBarConfig } from "../stroytorg-snackbar.models";

@Injectable({
    providedIn: 'root',
  })
  export class StroytorgSnackbarService {
    private snackbarSubject = new BehaviorSubject<SnackBarConfig>({} as SnackBarConfig);
    snackbarState$ = this.snackbarSubject.asObservable();
  
    showSuccess(message: string, duration?: number) {
      this.snackbarSubject.next({message, duration, success: true} as SnackBarConfig);
    }
  
    showError(message: string, duration?: number) {
      this.snackbarSubject.next({message, duration, success: false} as SnackBarConfig);
    }
  }
  
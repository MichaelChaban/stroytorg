import {
    HttpErrorResponse,
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpRequest,
    HttpStatusCode,
  } from '@angular/common/http';
  import { catchError, Observable, throwError } from 'rxjs';
  import { Injectable, inject } from '@angular/core';
import { StroytorgSnackbarService } from '@stroytorg/stroytorg-components';
  
  @Injectable()
  export class HttpErrorMessageInterceptor implements HttpInterceptor {
    snackbar = inject(StroytorgSnackbarService);
    intercept(
      req: HttpRequest<any>,
      next: HttpHandler
    ): Observable<HttpEvent<any>> {
      return next.handle(req).pipe(
        catchError((errorResponse: HttpErrorResponse) => {
          if (
            !errorResponse.ok &&
            ![HttpStatusCode.NotFound].includes(errorResponse.status)
          ) {
            if (errorResponse.status === 403) {
              this.snackbar.showError('Unauthorized request');
              console.error('Unauthorized request');
            } else {
              this.snackbar.showError(errorResponse.message);
              console.error(errorResponse);
            }
          }
  
          return throwError(() => errorResponse);
        })
      );
    }
  }
  
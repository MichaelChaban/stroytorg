import { Actions, concatLatestFrom, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, Observable, of, switchMap, tap, withLatestFrom } from 'rxjs';
import { select, Store } from '@ngrx/store';
import { Router } from '@angular/router';
import { BaseHttpService, Page, RequestParams } from '@stroytorg/shared';
import { createGetPageTableAction, createDataLoadSuccessTableAction, createDataLoadErrorTableAction, createFilterPageTableAction } from './actions';
// import { StroytorgSnackbarService } from '../../../services/gov-snackbar.service';

export interface CreateGetPageActionConfig<T> {
  service?: BaseHttpService<T>;
  fetchList?: (requestParams: RequestParams<any>) => Observable<Page<T>>;
  snackbar?: any; // StroytorgSnackbarService
  ngrxFeatureKey?: string;
  requireFilters?: boolean;
}

export function createGetPageTableEffect<T>(
  tableRepository: string,
  actions$: Actions,
  store$: Store<any>,
  config: CreateGetPageActionConfig<T>
) {
  return createEffect(() => {
    return actions$.pipe(
      ofType(
        createGetPageTableAction(tableRepository),
      ),
      switchMap(({ page, filter, sort, size }) => {
        const requestParams: RequestParams<any> = {
          page,
          filter,
          sort,
          size,
        }

        const fetchList$: Observable<Page<T>> =
          (config.service?.fetchList(requestParams) ??
           config.fetchList?.(requestParams)) as Observable<Page<T>>;
        if (!fetchList$) {
          throw new Error('Service or fetchList$ must by defined.');
        }
        return fetchList$.pipe(
          map(gPage => {
            const p = { ...gPage, number: page };
            return createDataLoadSuccessTableAction(tableRepository)({ gPage: p });
          }),
          catchError((error) => {
            config.snackbar?.showError(error.error.errors)
            return of(
              createDataLoadErrorTableAction(tableRepository)({ error })
            );
          })
        );
      })
    );
  });
}
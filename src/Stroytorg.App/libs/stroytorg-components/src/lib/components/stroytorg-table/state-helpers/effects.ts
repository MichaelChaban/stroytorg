import { Actions, concatLatestFrom, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, Observable, of, switchMap, tap, withLatestFrom } from 'rxjs';
import { select, Store } from '@ngrx/store';
import { Router } from '@angular/router';
import { BaseHttpService } from '@stroytorg/shared';
import { RequestParams, Page } from '../stroytorg-table.models';
import { createGetPageTableAction, createDataLoadSuccessTableAction, createDataLoadErrorTableAction, createFilterPageTableAction, createSortPageTableAction, badgeClosedTableAction } from './actions';
// import { StroytorgSnackbarService } from '../../../services/gov-snackbar.service';

export interface CreateGetPageActionConfig<T> {
  service?: BaseHttpService<T>;
  fetchList?: (requestParams: RequestParams<any>) => Observable<Page<T>>;
  snackbar?: any; // StroytorgSnackbarService
  ngrxFeatureKey?: string;
  requireFilters?: boolean;
}

export function createGetPageTableEffectNew<T>(
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



export function createGetPageTableEffect<T, SERVICE extends BaseHttpService<T>>(
  tableRepository: string,
  actions$: Actions,
  store$: Store<any>,
  httpService: SERVICE,
  requestParamsSelector: any
) {
  return createEffect(() =>
    { return actions$.pipe(
      ofType(
        createGetPageTableAction(tableRepository),
        createFilterPageTableAction(tableRepository),
        createSortPageTableAction(tableRepository),
        badgeClosedTableAction(tableRepository)
      ),
      withLatestFrom(store$.pipe(select(requestParamsSelector))),
      switchMap(([, requestParams]) => {
        return httpService.fetchList(requestParams).pipe(
          map((gPage) => {
            return createDataLoadSuccessTableAction(tableRepository)({ gPage });
          }),
          catchError((error) => {
            console.error('Error', error);
            return of(
              createDataLoadErrorTableAction(tableRepository)({ error })
            );
          })
        );
      })
    ) }
  );
}

export function createCloseBadgeRedirectEffect(
  tableRepository: string,
  actions$: Actions,
  selector: any,
  store: Store,
  router: Router
) {
  return createEffect(
    () => {
      return actions$.pipe(
        ofType(badgeClosedTableAction(tableRepository)),
        concatLatestFrom(() => store.select(selector)),
        tap(([, requestParams]) => {
          router.navigate([], {
            queryParams: (<any>requestParams)['filter'],
          });
        })
      );
    },
    { dispatch: false }
  );
}

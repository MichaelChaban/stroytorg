/* eslint-disable @typescript-eslint/no-explicit-any */
import { Actions, createEffect, ofType } from '@ngrx/effects';
import { catchError, map, Observable, of, switchMap } from 'rxjs';
import {
  createInitDetailAction,
  createLoadDetailFailureAction,
  createLoadDetailSuccessAction,
} from './detail.actions';
import { BaseHttpService } from '../api';
import { isNumber } from '../utils';

export interface CreateInitFormConfig<T> {
  service?: BaseHttpService<T>;
  snackbar?: any;
  get?: (obj: any) => Observable<T>;
}

export function createInitFormEffectNew<T>(
  detailsRepository: string,
  actions$: Actions,
  config: CreateInitFormConfig<T>
) {
  return createEffect(() =>
    { return actions$.pipe(
      ofType(createInitDetailAction(detailsRepository)),
      switchMap(({ obj }) => {
        const get$: Observable<any> = (config?.service?.get(obj as any) ??
          config?.get?.(obj)) as Observable<any>;
        return get$.pipe(
          map((item) => {
            return createLoadDetailSuccessAction(detailsRepository)({ item });
          }),
          catchError((error) => {
            config.snackbar?.showError(`Chyba: ${error}`);
            return of(
              createLoadDetailFailureAction(detailsRepository)({ error })
            );
          })
        );
      })
    ) }
  );
}

export function createInitDetailEffect<T, SERVICE extends BaseHttpService<T>>(
  detailsRepository: string,
  actions$: Actions,
  httpService: SERVICE
) {
  return createEffect(() => {
    return actions$.pipe(
      ofType(createInitDetailAction(detailsRepository)),
      switchMap(({ obj }) => {
        if (isNumber(obj)) {
          return httpService.get(obj as unknown as number).pipe(
            map((item) => {
              return createLoadDetailSuccessAction(detailsRepository)({ item });
            }),
            catchError((error) => {
              console.error('Error', error);
              return of(
                createLoadDetailFailureAction(detailsRepository)({ error })
              );
            })
          );
        } else {
          return of(
            createLoadDetailSuccessAction(detailsRepository)({ item: {} as T })
          );
        }
      })
    );
  });
}

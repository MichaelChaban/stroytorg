/* eslint-disable @typescript-eslint/no-explicit-any */
import { createAction, props } from '@ngrx/store';
import { FormState } from '../models';

export function createInitDetailAction(detailsRepository: string) {
  return createAction(
    `[${detailsRepository} Page] Init`,
    props<{ obj: string | null | number | undefined | any }>()
  );
}

export function createLoadDetailSuccessAction<T>(detailsRepository: string) {
  return createAction(
    `[${detailsRepository}/API] Load ${detailsRepository} Success`,
    props<{ item: T }>()
  );
}

export function createLoadDetailFailureAction(detailsRepository: string) {
  return createAction(
    `[${detailsRepository}/API] Load ${detailsRepository} Failure`,
    props<{ error: any }>()
  );
}

export function createUpdateDetailAction<T>(detailsRepository: string) {
  return createAction(
    `[${detailsRepository}/API] Update ${detailsRepository}`,
    props<{ item: T }>()
  );
}

export function createUpdateFormStateAction(detailsRepository: string) {
  return createAction(
    `[${detailsRepository}/API] Update Form State`,
    props<{ formState: FormState }>()
  );
}

export function createSaveDetailAction(detailsRepository: string) {
  return createAction(`[${detailsRepository}/API] Save Detail `);
}

export function createSaveDetailSuccessAction<T>(detailsRepository: string) {
  return createAction(
    `[${detailsRepository}/API] Save Detail Success`,
    props<{ item: T }>()
  );
}

export function createSaveDetailFailureAction(detailsRepository: string) {
  return createAction(
    `[${detailsRepository}/API] Save Detail Failure`,
    props<{ error: any }>()
  );
}

export function createUpdateSaveDetailAction(detailsRepository: string) {
  return createAction(`[${detailsRepository}/API] Update Detail `);
}

export function createClearTableAction(detailsRepository: string) {
  return createAction(`[${detailsRepository}/API] Clear Table`);
}

export function createResetStateAction(detailsRepository: string) {
  return createAction(`[${detailsRepository}/API] Reset State`);
}

export function createFilterPageTableAction<T>(tableRepository: string) {
  return createAction(
    `${tableRepository} | Refresh Page`,
    props<{ filter: Partial<T> }>()
  );
}

export function badgeClosedDetailAction<T>(detailRepository: string) {
  return createAction(
    `[${detailRepository} | Update Filter`,
    props<{ filter: Partial<T> }>()
  );
}

export function clearFilters<T>(detailRepository: string) {
  return createAction(
    `[${detailRepository} | Clear Filter`,
    props<{ item?: Partial<T> }>()
  );
}
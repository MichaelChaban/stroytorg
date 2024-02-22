import { createAction, props } from '@ngrx/store';
import { Sort, Page } from '../stroytorg-table.models';

/*
  TABLE
 */
enum TablePageAction {
  GET_PAGE = 'Get page',
  CREATE_STATIC_FILTER = 'Create static filter',
  DATA_LOAD_SUCCESS = 'Data load success',
  DATA_LOAD_ERROR = 'Data load error',
  REFRESH_PAGE = 'Refresh page',
  FILTER_PAGE = 'Filter page',
  SORT_PAGE = 'Sort page',
  DESTROY = 'Destroy',
  SELECT_ITEMS = 'Select items',
}

export function createGetPageTableAction<T = any>(tableRepository: string) {
  return createAction(
    `${tableRepository} ${TablePageAction.GET_PAGE}`,
    props<{
      page: number;
      size: number;
      id?: string | null;
      filter?: Partial<T>;
      sort?: Sort<T>[];
    }>()
  );
}

export function createStaticFilterTableAction<T = any>(tableRepository: string) {
  return createAction(
    `${tableRepository} ${TablePageAction.CREATE_STATIC_FILTER}`,
    props<{
      filter?: Partial<T>;
    }>()
  );
}

export function createDataLoadSuccessTableAction<T>(tableRepository: string) {
  return createAction(
    `${tableRepository} ${TablePageAction.DATA_LOAD_SUCCESS}`,
    props<{ gPage: Page<T> }>()
  );
}

export function createDataLoadErrorTableAction(tableRepository: string) {
  return createAction(
    `${tableRepository} ${TablePageAction.DATA_LOAD_ERROR}`,
    props<{ error: any }>()
  );
}

// TODO: use action in delete item effect
export function createRefreshPageTableAction(tableRepository: string) {
  return createAction(`${tableRepository} ${TablePageAction.REFRESH_PAGE}`);
}

export function createFilterPageTableAction<T>(tableRepository: string) {
  return createAction(
    `${tableRepository} ${TablePageAction.REFRESH_PAGE}`,
    props<{ filter: Partial<T> }>()
  );
}

export function createSortPageTableAction<T>(tableRepository: string) {
  return createAction(
    `${tableRepository} ${TablePageAction.SORT_PAGE}`,
    props<{ sort: Sort<T>[] }>()
  );
}
export function createDestroyTableAction(tableRepository: string) {
  return createAction(`${tableRepository} ${TablePageAction.DESTROY}`);
}

export function badgeClosedTableAction<T>(tableRepository: string) {
  return createAction(
    `[${tableRepository} | Update Filter`,
    props<{ filter: Partial<T> }>()
  );
}

export function createSelectItemsTableAction<T>(tableRepository: string) {
  return createAction(
    `${tableRepository} ${TablePageAction.SELECT_ITEMS}`,
    props<{ selectedItems: T[] }>()
  );
}

import { DetailState } from "@stroytorg/shared";
import { DEFAULT_SEARCH_PARAMS } from "../models/datasource.models";
import { RequestParams, Page } from "../stroytorg-table.models";

export interface TableState<T> {
  dataLoading: boolean;
  requestParams: RequestParams<T>;
  gPage?: Page<T>;
  error?: string | null;
  selectedItems?: T[];
  submitted?: boolean;
}

export const INITIAL_TABLE_STATE: TableState<any> = {
  gPage: {
    list: [],
    // size: DEFAULT_SEARCH_LIMIT,
    count: 0.
  } as unknown as Page<any>,
  dataLoading: false,
  requestParams: DEFAULT_SEARCH_PARAMS,
  submitted: false
};

export const INITIAL_SUBMIT_DIALOG_STATE: DetailState<any> = {
  loaded: false,
};

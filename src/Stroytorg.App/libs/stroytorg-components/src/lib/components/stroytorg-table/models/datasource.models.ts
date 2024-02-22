import { RequestParams } from "@stroytorg/shared";

export const DEFAULT_SEARCH_LIMIT = 50;

export const DEFAULT_SEARCH_PARAMS: RequestParams<any> = {
  page: 1,
  size: DEFAULT_SEARCH_LIMIT,
  id: '',
};

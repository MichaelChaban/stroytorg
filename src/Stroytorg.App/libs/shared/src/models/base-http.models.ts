/* eslint-disable @typescript-eslint/no-explicit-any */
import { HttpParams, HttpResponse } from '@angular/common/http';
import * as moment from 'moment';

export function requestParamsToHttpParams<T>(
  requestParams: RequestParams<T>,
  httpParams: HttpParams = new HttpParams()
): HttpParams {
  httpParams = httpParams
    .append('Limit', requestParams.size.toString())
    .append('Offset', (requestParams.page * requestParams.size).toString());
  if (requestParams.id != '') {
    httpParams = httpParams.append('Id', requestParams.id as any);
  }

  httpParams = getFilter(requestParams, httpParams);
  httpParams = getSort(requestParams, httpParams);

  return httpParams;
}

function getFilter<T>(
  requestParams: RequestParams<any>,
  httpParams: HttpParams
): HttpParams {
  if (!requestParams.filter) {
    return httpParams;
  }
  Object.keys(requestParams?.filter).forEach((key) => {
    if (requestParams.filter?.[key as keyof Partial<T>]) {
      httpParams = httpParams.append(
        'Filter.' + key,
        transformData(requestParams.filter?.[key as keyof Partial<T>])
      );
    }
  });
  return httpParams;
}

function getSort(
  requestParams: RequestParams<any>,
  httpParams: HttpParams
): HttpParams {
  if (!requestParams.sort) {
    return httpParams;
  }

  Object.keys(requestParams?.sort).forEach((key, value) => {
    if (requestParams.sort?.[value]) {
      httpParams = httpParams
        .append('Sort.Active', key)
        .append('Sort.Direction', value);
    }
  });
  return httpParams;
}

function transformData(data: any) {
  if (data) {
    if (moment.isMoment(data)) {
      data = moment(data).toDate().toISOString();
    }
  }
  return data;
}

export function saveFile(response: HttpResponse<Blob>) {
  const fileName = response.headers
    .get('Content-Disposition')
    ?.split(';')[1]
    .split('=')[1];
  const blob: Blob = response.body as Blob;
  const a = document.createElement('a');
  a.download = fileName ?? 'undefined';
  a.href = window.URL.createObjectURL(blob);
  a.click();
}

export declare interface Sort<T> {
  id: string;
  sortDirection: SortDirection;
}

export declare type SortDirection = 'asc' | 'desc' | undefined;

export interface RequestParams<T> {
  page: number;
  size: number;
  id?: string;
  filter?: Partial<T>;
  sort?: Sort<T>[];
}

export interface Page<T> {
  list: T[];
  count: number;
  size: number;
  number: number;
}
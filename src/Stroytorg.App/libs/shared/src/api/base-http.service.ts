/* eslint-disable @typescript-eslint/no-explicit-any */
import { HttpClient } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { Injectable } from '@angular/core';
import { ClearUtils } from '../utils/clear.utils';
import { Page, RequestParams, requestParamsToHttpParams } from '../models';

@Injectable({ providedIn: 'root' })
export abstract class BaseHttpService<T, TKey = number> {
  constructor(protected readonly http: HttpClient) {}

  fetchList(requestParams: RequestParams<T>): Observable<Page<T>> {
    const params = requestParamsToHttpParams<T>(requestParams);
    return this.http.get<Page<T>>('this.basePath', { params }).pipe(
      map((response: Page<T>) => {
        response.size = requestParams.size;
        return response;
      })
    );
  }
  
  get(id: TKey, url?: string): Observable<T> {
    const newUrl = url ? `/${url}/${id}` : `/${id}`;
    return this.http.get<T>(newUrl);
  }

  protected createParams<T>(requestParams: RequestParams<T>): any {
    const sorter = this.createSorter(requestParams);
    const filter = JSON.parse(JSON.stringify(requestParams.filter));
    const transformedFilter = filter;
    ClearUtils.recursiveObjectAttributesDeletation(transformedFilter);
    return {
      paging: {
        page: requestParams.page,
        size: requestParams.size,
      },
      ...(sorter && { sorting: sorter }),
      ...(transformedFilter && { filter: transformedFilter }),
    };
  }

  private createSorter<T>(requestParams: RequestParams<T>) {
    if (requestParams?.sort?.length === 1) {
      const sorter = requestParams.sort[0];
      return {
        sortField: sorter.id,
        sortOrder: sorter.sortDirection === 'asc' ? 1 : -1,
      };
    }
    return null;
  }
}

/* eslint-disable @typescript-eslint/no-explicit-any */
import { ActionCreator, createReducer, on, ReducerTypes } from '@ngrx/store';
import {
  badgeClosedDetailAction,
  clearFilters,
  createInitDetailAction,
  createLoadDetailFailureAction,
  createLoadDetailSuccessAction,
  createResetStateAction,
  createSaveDetailAction,
  createSaveDetailFailureAction,
  createSaveDetailSuccessAction,
  createUpdateDetailAction,
  createUpdateFormStateAction,
} from './detail.actions';
import {
  DetailState,
  INITIAL_DETAIL_STATE,
  INITIAL_SAVE_ITEM_STATE,
} from './detail.models';

export function createDetailReducers<
  T = any,
  TState extends DetailState<T> = DetailState<T>
>(
  detailRepository: string,
  initialState: TState = INITIAL_DETAIL_STATE as unknown as TState,
  ...ons: ReducerTypes<TState, readonly ActionCreator[]>[]
) {
  return createReducer(
    initialState,
    on(createInitDetailAction(detailRepository), (state) => {
      return {
        ...state,
        loaded: false,
        error: null,
      };
    }),

    on(createLoadDetailSuccessAction(detailRepository), (state, { item }) => {
      return {
        ...state,
        loaded: true,
        item,
      };
    }),

    on(createLoadDetailFailureAction(detailRepository), (state, { error }) => {
      return {
        ...state,
        loaded: true,
        error,
      };
    }),

    on(createUpdateDetailAction(detailRepository), (state, { item }) => {
      return {
        ...state,
        item,
        formState: {
          dirty: true,
          valid: state.formState?.valid ?? true,
        },
      };
    }),

    on(
      createUpdateFormStateAction(detailRepository),
      (state, { formState }) => {
        return {
          ...state,
          formState,
        };
      }
    ),

    on(createSaveDetailSuccessAction(detailRepository), (state) => {
      return {
        ...state,
        formState: {
          dirty: false,
          valid: state.formState?.valid ?? true,
        },
      };
    }),
    on(badgeClosedDetailAction(detailRepository), (state, { filter }) => {
      const newItem = Object.entries(<any>state?.item).reduce(
        (acc, [key, value]) => {
          if (filter[key as keyof typeof filter] === value) {
            return { ...acc, [key]: null };
          }
          return { ...acc, [key]: value };
        },
        {}
      );
      return {
        ...state,
        item: newItem,
        formState: {
          dirty: true,
          valid: state.formState?.valid ?? true,
        },
      };
    }),
    on(clearFilters(detailRepository), (state, { item }) => {
      const newItem = setPropertiesToNull(item);
      return {
        ...state,
        item: newItem,
        formState: {
          dirty: true,
          valid: state.formState?.valid ?? true,
        },
      };
    }),
    on(createResetStateAction(detailRepository), () => {
      return {
        ...(initialState as any),
      };
    }),
    ...ons
  );
}

function setPropertiesToNull(item: any): any {
  if (item) {
    const nullItem = { ...item };

    for (const propName in nullItem) {
      nullItem[propName] = null;
    }
    return nullItem;
  }

  return item;
}

export function createSaveDetailReducers(
  detailRepository: string,
  initialState = INITIAL_SAVE_ITEM_STATE
) {
  return createReducer(
    initialState,

    on(createSaveDetailAction(detailRepository), (state) => {
      return {
        ...state,
        saveInProgress: true,
      };
    }),

    on(createSaveDetailSuccessAction(detailRepository), (state, { item }) => {
      return {
        ...state,
        item,
        saveInProgress: false,
      };
    }),

    on(createSaveDetailFailureAction(detailRepository), (state, { error }) => {
      return {
        ...state,
        error,
        saveInProgress: false,
      };
    })
  );
}

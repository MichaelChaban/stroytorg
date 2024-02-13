export enum SelectSize {
  XSMALL = 'x-small-width',
  SMALL = 'small-width',
  DEFAULT = 'default-width',
  LARGE = 'large-width',
  XLARGE = 'x-large-width',
  FULL = 'full-width',
};

export type CompareWithFn = (o1: any, o2: any) => boolean;

export function compareWithId(o1: any, o2: any): boolean {
  return o1?.id == o2?.id;
}

export interface SelectableItem<T> {
  label: string;
  value: T;
  selected: boolean;
}

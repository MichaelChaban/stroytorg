export type SelectSize = 'default' | 'large' | 'xlarge';

export const SelectSize = {
  default: 'classic' as SelectSize,
  large: 'large' as SelectSize,
  xlarge: 'x-large' as SelectSize,
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

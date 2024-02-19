export interface PaginatorPageModel {
  page: number;
  visible: boolean;
  disabled?: boolean;
  space?: boolean;
}

export const PAGE_SIZE_OPTIONS = [
  { label: '10', value: 10 },
  { label: '20', value: 20 },
  { label: '50', value: 50 },
  {label: '100', value: 100},
];

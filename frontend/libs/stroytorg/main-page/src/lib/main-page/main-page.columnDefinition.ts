import { ColumnsDefinition } from '@frontend/shared/domain';

export function getMainPageTableColumnDefinitions(): ColumnsDefinition[] {
  return [
    {
      headerName: 'Id',
      value: (row: any) => row.id,
    },
    {
      headerName: 'Name',
      value: (row: any) => row.name,
    },
    {
      headerName: 'Age',
      value: 'age',
    },
    {
      headerName: 'Year',
      value: 'year',
    },
    {
      headerName: 'Name',
      value: (row: any) => row.name,
    },
    {
      headerName: 'Name',
      value: (row: any) => row.name,
    },
    {
      headerName: 'Name',
      value: (row: any) => row.name,
    },
    {
      headerName: 'Name',
      value: (row: any) => row.name,
    },
    {
      headerName: 'Name',
      value: (row: any) => row.name,
    },
  ];
}
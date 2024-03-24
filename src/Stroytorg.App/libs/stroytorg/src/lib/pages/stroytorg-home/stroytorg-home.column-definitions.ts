import {
  ButtonDefinition,
  CardDefinition,
  ColumnDefinition,
} from '@stroytorg/stroytorg-components';
import { FilterDefinition } from 'libs/stroytorg-components/src/lib/components/stroytorg-card/components';

export interface SomeModel {
  id: number;
  name: string;
  email: string;
}

export interface SomeRowDataModel {
  id: number;
  name: string;
  description: string;
}

export function getColumnDefinitions(): ColumnDefinition<SomeModel>[] {
  return [
    {
      id: 'name',
      value: (row?: SomeModel) => row?.name,
      headerName: 'Name',
      sortable: true,
    },
    {
      id: 'email',
      value: 'email',
      headerName: 'Email',
      sortable: true,
    },
    {
      id: 'actions',
      columnActions: [
        {
          icon: 'task_alt',
          rounded: true,
          buttonStyle: 'raised-button',
          buttonPalette: 'default-button',
          onClick: (row: SomeModel) => {
            console.log('Edit', row);
          },
          buttonClass: 'p-0',
        },
        {
          title: 'Delete',
          buttonStyle: 'raised-button',
          buttonPalette: 'danger-button',
          onClick: (row: SomeModel) => {
            console.log('Delete', row);
          },
          disabled: (row: SomeModel) => row.name === 'John Doe',
        },
      ],
    },
  ];
}

export function getCardRowDefinition(): CardDefinition {
  return {
    cardRowDefinition: [
      {
        title: 'name',
        content: (row: SomeRowDataModel) => row.name,
      },
      {
        imageName: 'Volcano.jpg',
      },
      {
        title: 'Description',
      },
      {
        content: (row: SomeRowDataModel) => row.description,
      },
      {
        cardRowActions: [
          {
            title: 'Buy',
            icon: 'search',
            buttonPalette: 'primary-button',
            buttonStyle: 'basic-button',
            onClick: (row: SomeModel) => {
              console.log('Delete', row);
            },
          },
          {
            title: 'Delete',
            buttonPalette: 'primary-button',
            buttonStyle: 'flat-button',
            onClick: (row: SomeModel) => {
              console.log('Delete', row);
            },
          },
        ] as ButtonDefinition[],
      },
    ],
    cardDisabled: (row: SomeRowDataModel) => row.name === 'John Doe',
  };
}

export const mockData: SomeModel[] = [
  { id: 1, name: 'John Doe', email: 'john@example.com' },
  { id: 2, name: 'Jane Smith', email: 'jane@example.com' },
  { id: 3, name: 'Alice Johnson', email: 'alice@example.com' },
  { id: 4, name: 'Bob Brown', email: 'bob@example.com' },
  { id: 5, name: 'Emily Davis', email: 'emily@example.com' },
  { id: 1, name: 'John Doe', email: 'john@example.com' },
  { id: 2, name: 'Jane Smith', email: 'jane@example.com' },
  { id: 3, name: 'Alice Johnson', email: 'alice@example.com' },
  { id: 4, name: 'Bob Brown', email: 'bob@example.com' },
  { id: 5, name: 'Emily Davis', email: 'emily@example.com' },
  { id: 5, name: 'Emily Davis', email: 'emily@example.com' },
  { id: 1, name: 'John Doe', email: 'john@example.com' },
  { id: 2, name: 'Jane Smith', email: 'jane@example.com' },
  { id: 3, name: 'Alice Johnson', email: 'alice@example.com' },
  { id: 4, name: 'Bob Brown', email: 'bob@example.com' },
  { id: 5, name: 'Emily Davis', email: 'emily@example.com' },
  // Add more mock data as needed
];

export const cardMockData: SomeRowDataModel[] = [
  {
    id: 1,
    name: 'John Doe',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 2,
    name: 'Jane Smith',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 3,
    name: 'Alice Johnson',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 4,
    name: 'Bob Brown',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 5,
    name: 'Emily Davis',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 1,
    name: 'John Doe',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 1,
    name: 'John Doe',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 2,
    name: 'Jane Smith',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 3,
    name: 'Alice Johnson',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 4,
    name: 'Bob Brown',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 5,
    name: 'Emily Davis',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 1,
    name: 'John Doe',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 1,
    name: 'John Doe',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 2,
    name: 'Jane Smith',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 3,
    name: 'Alice Johnson',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 4,
    name: 'Bob Brown',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 5,
    name: 'Emily Davis',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 1,
    name: 'John Doe',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 1,
    name: 'John Doe',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 2,
    name: 'Jane Smith',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 3,
    name: 'Alice Johnson',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 4,
    name: 'Bob Brown',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 5,
    name: 'Emily Davis',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
  {
    id: 1,
    name: 'John Doe',
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
  },
];

export function getCardFilterDefinitions(): FilterDefinition[] {
  return [
    {
      type: 'text',
      label: 'Name',
      description: 'Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry',
      value: '',
    },
    {
      type: 'text',
      label: 'Description',
      value: '',
    },
    {
      type: 'range',
      label: 'Price range',
      value: '',
    },
    {
      type: 'boolean',
      label: 'Is in stock?',
      value: '',
    },
    {
      type: 'select',
      label: 'Select',
      value: 1,
      options: [
        {
          label: 'Option 1',
          value: 1,
        },
        {
          label: 'Option 2',
          value: 2,
        },
        {
          label: 'Option 3',
          value: 3,
        }
      ]
    },
  ];
}

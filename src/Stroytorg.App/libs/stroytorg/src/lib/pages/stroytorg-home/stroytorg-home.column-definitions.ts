import { Icon } from "@stroytorg/shared";
import { ButtonDefinition, ButtonStyle, CardRowDefinition, ColumnDefinition } from "@stroytorg/stroytorg-components";

export interface SomeModel{
    id: number;
    name: string;
    email: string;
}

export interface SomeRowDataModel{
    id: number;
    name: string;
    description: string;
}

export function getColumnDefinitions() : ColumnDefinition<SomeModel>[]{
    return [

        {
            id: 'name',
            value: (row?: SomeModel) => row?.name,
            headerName: 'Name',
            sortable: true
        },
        {
            id: 'email',
            value: 'email',
            headerName: 'Email',
            sortable: true
        },
        {
            id: 'actions',
            columnActions: [
                {
                    icon: Icon.TASK_ALT,
                    buttonStyle: ButtonStyle.DEFAULT,
                    onClick: (row: SomeModel) => {
                        console.log('Edit', row);
                    }
                },
                {
                    title: 'Delete',
                    buttonStyle: ButtonStyle.DANGER,
                    onClick: (row: SomeModel) => {
                        console.log('Delete', row);
                    }
                }
            ]
        }
    ];
}

export function getCardRowDefinition() : CardRowDefinition[]{
    return [
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
                    icon: Icon.SEARCH,
                    buttonStyle: ButtonStyle.DEFAULT,
                    onClick: (row: SomeModel) => {
                        console.log('Delete', row);
                    }
                },
                {
                    title: 'Delete',
                    buttonStyle: ButtonStyle.PRIMARY,
                    onClick: (row: SomeModel) => {
                        console.log('Delete', row);
                    }
                }
            ] as ButtonDefinition[]
        }
    ];
}

export const mockData: SomeModel[] = [
    { id: 1, name: "John Doe", email: "john@example.com" },
    { id: 2, name: "Jane Smith", email: "jane@example.com" },
    { id: 3, name: "Alice Johnson", email: "alice@example.com" },
    { id: 4, name: "Bob Brown", email: "bob@example.com" },
    { id: 5, name: "Emily Davis", email: "emily@example.com" },
    { id: 1, name: "John Doe", email: "john@example.com" },
    { id: 2, name: "Jane Smith", email: "jane@example.com" },
    { id: 3, name: "Alice Johnson", email: "alice@example.com" },
    { id: 4, name: "Bob Brown", email: "bob@example.com" },
    { id: 5, name: "Emily Davis", email: "emily@example.com" },
    { id: 5, name: "Emily Davis", email: "emily@example.com" },
    { id: 1, name: "John Doe", email: "john@example.com" },
    { id: 2, name: "Jane Smith", email: "jane@example.com" },
    { id: 3, name: "Alice Johnson", email: "alice@example.com" },
    { id: 4, name: "Bob Brown", email: "bob@example.com" },
    { id: 5, name: "Emily Davis", email: "emily@example.com" }
    // Add more mock data as needed
];


export const cardMockData: SomeRowDataModel[] = [
    { id: 1, name: "John Doe", description: "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged." },
    { id: 2, name: "Jane Smith", description: "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged." },
    { id: 3, name: "Alice Johnson", description: "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged." },
    { id: 4, name: "Bob Brown", description: "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged." },
    { id: 5, name: "Emily Davis", description: "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged." },
    { id: 1, name: "John Doe", description: "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged." },
];
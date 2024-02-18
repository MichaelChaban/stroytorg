import { Icon } from "@stroytorg/shared";
import { ButtonStyle, ColumnDefinition } from "@stroytorg/stroytorg-components";

export interface SomeModel{
    id: number;
    name: string;
    email: string;
}

export function getColumnDefinitions() : ColumnDefinition<SomeModel>[]{
    return [

        {
            id: 'name',
            value: (row?: SomeModel) => row?.name,
            headerName: 'Name',
            type: 'string',
            sortable: true
        },
        {
            id: 'email',
            value: 'email',
            headerName: 'Email',
            type: 'string',
            sortable: true
        },
        {
            id: 'actions',
            type: 'actions',
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
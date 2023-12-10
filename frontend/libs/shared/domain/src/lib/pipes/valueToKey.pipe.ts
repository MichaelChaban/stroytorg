import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'valueToKey',
    pure: false,
    standalone: true,
})
export class ValueToKeyPipe implements PipeTransform {
    transform(value: any, collection: {label: string, value: any}[]): string {
        return Array.from(collection).find(x => x.value == value)?.label ?? '';
    }
}

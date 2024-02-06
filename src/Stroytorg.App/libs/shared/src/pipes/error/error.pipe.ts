import { Pipe, PipeTransform } from '@angular/core';
import { ValidationErrors } from '@angular/forms';
import { ErrorCodesFns } from './error.codes';

@Pipe({
  name: 'error',
  standalone: true,
})
export class ErrorPipe implements PipeTransform {
  transform(errors: ValidationErrors | undefined | null): string[] {
    const errorMessages: string[] = [];

    if (!errors) {
      return errorMessages;
    }

    for (const errorKey in errors) {
      const errorFn = ErrorCodesFns[errorKey];
      if (!errorFn) {
        errorMessages.push('Unknown error');
      } else {
        errorMessages.push(ErrorCodesFns[errorKey](errors[errorKey]));
      }
    }

    return errorMessages;
  }
}
import { Pipe, PipeTransform } from '@angular/core';
import { ValidationErrors } from '@angular/forms';
import { getFirstError, ErrorCodes } from './error.model';

const EMPTY = '';

@Pipe({
  name: 'error',
  standalone: true,
  pure: false,
})
export class ErrorPipe implements PipeTransform {
  transform(errors: ValidationErrors | undefined | null): string {
    if (!errors) {
      return EMPTY;
    }
    const error = getFirstError(errors);
    if (error) {
      const errorFn = ErrorCodes[error.key];
      if (!errorFn) {
        return 'Невизначена помилка';
      }
      return ErrorCodes[error.key](error.value);
    }
    return EMPTY;
  }
}

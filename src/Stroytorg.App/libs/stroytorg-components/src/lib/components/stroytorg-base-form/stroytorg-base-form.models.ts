export interface FormState {
  dirty: boolean;
  valid: boolean;
}

export function isFormStateEqual(current: FormState, other: FormState) {
  if (!current && !other) {
    return true;
  }

  if (current && other) {
    return current.dirty === other.dirty && current.valid === other.valid;
  }

  return false;
}

export type InputSize =
  | 'x-small-width'
  | 'small-width'
  | 'default-width'
  | 'large-width'
  | 'x-large-width'
  | 'full-width';

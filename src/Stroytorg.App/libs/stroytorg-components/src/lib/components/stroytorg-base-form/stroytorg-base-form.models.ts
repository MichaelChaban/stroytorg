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

export enum InputSize {
  XSMALL = 'x-small-width',
  SMALL = 'small-width',
  DEFAULT = 'default-width',
  LARGE = 'large-width',
  XLARGE = 'x-large-width',
  FULL = 'full-width',
};
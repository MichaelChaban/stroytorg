type ErrorCodesFn = (value?: any) => string;

export const ErrorCodesFns: { [key: string]: ErrorCodesFn } = {
  required: () => `ERROR_CODES.POVINNE_POLE`,
  email: (value) => value.message,
  rcFormatInvalid: () => 'ERROR_CODES.RC_NEPLATNY_FORMAT',
  rcIsInvalid: () => 'ERROR_CODES.RC_NEPLATNE',
};

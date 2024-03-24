export function isAllPropertyEmpty(obj: any): boolean {
  return Object.values(obj).every((x) => {
    return (
      x === null || x === '' || (Array.isArray(x) ? x?.length === 0 : false)
    );
  });
}

export function isAllPropertyEmptyRecursive(obj: any): boolean {
  return Object.values(obj).every((x) => {
    return (
      x === null ||
      x === '' ||
      (Array.isArray(x) ? x?.length === 0 : false) ||
      (typeof x === 'object' ? isAllPropertyEmptyRecursive(x) : false)
    );
  });
}

export function isObject(obj: any): boolean {
  return typeof obj === 'object' && !Array.isArray(obj) && obj !== null;
}

export function isAllPropertyEmptyRecursiveWithException(
  obj: any,
  exception: string[] = []
): boolean {
  if (obj === null || obj === undefined) {
    return true;
  }
  return Object.keys(obj).every((x) => {
    if (exception.includes(x)) {
      return true;
    }
    if (obj[x] === null || obj[x] === undefined || obj[x] === '') {
      return true;
    }
    if (Array.isArray(obj[x])) {
      return obj[x]?.length === 0;
    }
    if (isObject(obj[x])) {
      return isAllPropertyEmptyRecursiveWithException(obj[x], exception);
    }
    return obj[x] === null || obj[x] === '';
  });
}

export function deleteEmptyNullorUndefinedProperties(obj: any): any {
  if (obj === null || obj === undefined) {
    return obj;
  }
  Object.keys(obj).forEach((key) => {
    if (obj[key] === null || obj[key] === undefined || obj[key] === '') {
      delete obj[key];
    } else if (typeof obj[key] === 'object') {
      deleteEmptyNullorUndefinedProperties(obj[key]);
    }
  });
  return obj;
}

export function getBooleanValue(value: any): boolean {
  if (value === null || value === undefined) {
    return false;
  }
  if (typeof value === 'boolean') {
    return value;
  }
  if (typeof value === 'string') {
    return value.toLowerCase() === 'true';
  }
  return false;
}

export function isNumber(value: any): boolean {
  return typeof value === 'number' && isFinite(value);
}

export function isAllValuesEmpty(values: any[]): boolean {
  return values.every((x) => {
    return x === null || x === '' || x === undefined;
  });
}

export function isAllValuesFilled(values: any[]): boolean {
  return values.every((x) => {
    return x !== null && x !== '' && x !== undefined;
  });
}

export function isAllEmptyOrAllFilled(values: any[]): boolean {
  return (
    isAllValuesEmpty(values) ||
    isAllValuesFilled(values)
  );
}

export function countFilledPropertiesRecursive(
  obj: any,
  config?: {
    countedKeysAsFalse?: string[];
    notCountedKeys?: string[];
  }
): number {
  if (obj === null || obj === undefined) {
    return 0;
  }
  return Object.keys(obj).reduce((acc, key) => {
    if (config?.countedKeysAsFalse?.includes(key)) {
      if (obj[key] === null || obj[key] === undefined || obj[key] === '') {
        return acc;
      }
    } else {
      if (
        obj[key] === null ||
        obj[key] === undefined ||
        obj[key] === '' ||
        obj[key] === false
      ) {
        return acc;
      }
    }
    if (Array.isArray(obj[key])) {
      return acc + obj[key].length;
    }
    if (isObject(obj[key])) {
      return acc + countFilledPropertiesRecursive(obj[key]);
    }
    if (!config?.notCountedKeys?.includes(key)) {
      return acc + 1;
    } else {
      return acc;
    }
  }, 0);
}

export function setAllPropertiesToUndefinedRecursive(obj: any): void {
  if (obj === null || obj === undefined) {
    return;
  }
  Object.keys(obj).forEach((key) => {
    if (Array.isArray(obj[key])) {
      obj[key] = [];
    } else if (isObject(obj[key])) {
      setAllPropertiesToUndefinedRecursive(obj[key]);
    } else {
      obj[key] = null;
    }
  });
  return obj;
}

export function dotNotationToNestedObjectRecursive(obj: any) {
  if (!obj) {
    return null;
  }
  const result = {};

  for (const objectPath in obj) {
    const parts = objectPath.split('.');

    let target: any = result;
    while (parts.length > 1) {
      const part: any = parts.shift();
      target = target[part] = target[part] || {};
    }

    // Set value at end of path
    target[parts[0]] = obj[objectPath]
  }

  return result;
}

export function objectsEqual(obj1: any, obj2: any): boolean {
  if (obj1 === obj2) {
    return true;
  }

  if (!isObject(obj1) || !isObject(obj2)) {
    return false;
  }

  const keys1 = Object.keys(obj1);
  const keys2 = Object.keys(obj2);

  if (keys1.length !== keys2.length) {
    return false;
  }

  for (const key of keys1) {
    if (!objectsEqual(obj1[key], obj2[key])) {
      return false;
    }
  }

  return true;
}

export function getPropertyByPath<T>(item: T, path: string) {
  if (isObject(item)) {
    return path ? item[path as keyof T] : item;
  } else {
    return item;
  }
}
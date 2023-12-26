import { NgControl } from '@angular/forms';

export abstract class StroytorgBaseInputControls<T> {
  value?: T | null;

  readonly ngControl!: NgControl | null;

  readonly label!: string;

  readonly required!: boolean;

  readonly disabled!: boolean;
}

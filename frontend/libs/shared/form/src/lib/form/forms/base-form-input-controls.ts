import { NgControl } from "@angular/forms";

export abstract class BaseInputControls {

    readonly ngControl?: NgControl | null;

    readonly label!: string | null;

    readonly required!: boolean;

    readonly readonly!: boolean;
}
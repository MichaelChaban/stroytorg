import { FormControl, FormGroup } from "@angular/forms";
import { DatePickerDate } from "./models";

export function createFormGroup(date: DatePickerDate){
    return new FormGroup({
        day: new FormControl(date.monthDayNumber),
        month: new FormControl(date.monthIndex),
        year: new FormControl(date.year),
      });
}

export function patchValueToModel(form: FormGroup, date: DatePickerDate){
    form.get('day')?.setValue(date.monthDayNumber);
    form.get('month')?.setValue(date.monthIndex);
    form.get('year')?.setValue(date.year);
}
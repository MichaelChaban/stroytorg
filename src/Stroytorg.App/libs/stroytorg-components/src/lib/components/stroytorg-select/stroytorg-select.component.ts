import {
  Component,
  Input,
  Output,
  EventEmitter,
  OnChanges,
  SimpleChanges,
  Optional,
  Self,
} from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  ControlValueAccessor,
  NgControl,
  ReactiveFormsModule,
} from '@angular/forms';
import {
  CompareWithFn,
  SelectSize,
  SelectableItem,
  compareWithId,
} from './stroytorg-select.models';
import { ErrorPipe, ObjectUtils } from '@stroytorg/shared';
import {
  StroytorgBaseInputControls,
  StroytorgBaseFormInputComponent,
} from '../stroytorg-base-form';
import { NgSelectModule } from '@ng-select/ng-select';

@Component({
  selector: 'stroytorg-select',
  standalone: true,
  imports: [CommonModule, ErrorPipe, ReactiveFormsModule, NgSelectModule],
  templateUrl: './stroytorg-select.component.html',
  styleUrls: ['./stroytorg-select.component.scss'],
  providers: [
    {
      provide: StroytorgBaseInputControls,
      useExisting: StroytorgSelectComponent,
    },
  ],
})
export class StroytorgSelectComponent<T>
  extends StroytorgBaseFormInputComponent
  implements ControlValueAccessor, OnChanges
{
  @Input()
  bindLabel: string = 'label';

  @Input()
  bindValue: string = 'value';

  @Input()
  size = SelectSize.DEFAULT as string;

  @Input()
  compareWith!: CompareWithFn;

  @Input()
  items!: T[];

  @Output() valueChange = new EventEmitter<string>();

  filter?: string = '';

  protected _options!: SelectableItem<T>[];

  constructor(@Optional() @Self() ngControl: NgControl) {
    super(ngControl);
  }

  ngOnChanges(_changes: SimpleChanges): void {
    if (_changes['items'] && this.items) {
      const options = this.createOptions(this.items);
      this._options = this.setOptionSelection(
        options,
        this.ngControl?.value,
        undefined
      );
    }
    this.compareWith = compareWithId;
  }

  selectionChange(event: any) {
    if (!event || !event.target) {
      return;
    }

    const selectedIndex = Number.parseInt(event.target.value);
    this._options = this.setOptionSelection(
      this._options,
      undefined,
      selectedIndex
    );

    if (this.bindValue) {
      console.log(selectedIndex);
      const optionValue: any = this._options[selectedIndex].value;
      const value = optionValue?.[this.bindValue as string] ?? optionValue;
      this.formControl.setValue(value);
    } else {
      this.formControl.setValue(this._options[selectedIndex].value);
    }
  }

  private getOptionValue(item: T) {
    return ObjectUtils.getPropertyByPath(item, this.bindValue);
  }

  private getOptionLabel(item: T) {
    return ObjectUtils.getPropertyByPath(item, this.bindLabel);
  }

  private createOptions(items: T[]) {
    return (items ?? []).map(
      (x) =>
        ({
          label: this.getOptionLabel(x),
          value: this.getOptionValue(x),
          selected: false,
        } as SelectableItem<T>)
    );
  }

  private setOptionSelection(
    options: SelectableItem<T>[],
    value?: T,
    selectedIndex?: number
  ) {
    return (options ?? []).map((x, index) => {
      const selected =
        index === selectedIndex || ObjectUtils.objectsEqual(x.value, value);
      return x.selected === selected ? x : { ...x, selected };
    });
  }
}

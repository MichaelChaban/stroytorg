import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CardComponent } from '@frontend/shared/card';
import { ButtonStyle, CardDefinition, CardElementType, Icons } from '@frontend/shared/domain';

@Component({
  selector: 'frontend-main-page',
  standalone: true,
  imports: [ CommonModule, CardComponent ],
  templateUrl: './main-page.component.html',
  styleUrls: ['./main-page.component.scss'],
})
export class MainPageComponent {
  
  cardDefinition: CardDefinition[] = [
    {
      display: 'block',
      elementType: CardElementType.LABEL,
      label: 'id',
    },
    {
      display: 'block',
      elementType: CardElementType.CONTENT,
      content: (row: any) => row.name,
    },
    {
      elementType: CardElementType.ACTION,
      display: 'block',
      actions: [
        {
          buttonStyle: ButtonStyle.PRIMARY_BUTTON,
          icon: Icons.HOME,
          label: 'Детальніше',
          onClick: this.buyProduct,
          tooltip: { tooltipPosition: 'above', tooltipText: 'Детальніше' },
        }
      ]
    },
  ];

  elementsInRowCount = 1;

  data: any[] = [
    {
      id: 1,
      name: "Name1"
    },
    {
      id: 2,
      name: "Name2"
    }
  ];

  buyProduct(smth: any){
    console.log("Called fn", smth);
  }
}

import { ElementRef, Renderer2 } from "@angular/core";

export function setDefaultButton(el: ElementRef, renderer: Renderer2){
    renderer.setStyle(el.nativeElement, 'background-color', '#f26a31');
    renderer.setStyle(el.nativeElement, 'color', '#ffffff');
    renderer.setStyle(el.nativeElement, 'border', 'none');
    renderer.setStyle(el.nativeElement, 'border-radius', '4px');
    renderer.setStyle(el.nativeElement, 'padding', '0.75rem 2rem');
    renderer.setStyle(el.nativeElement, 'font-family', 'Nunito Sans');
    renderer.setStyle(el.nativeElement, 'font-weight', 'bold');
    renderer.setStyle(el.nativeElement, 'transition', '0.2s');

    renderer.listen(el.nativeElement, 'mouseenter', () => {
      renderer.setStyle(el.nativeElement, 'background-color', '#eb906a');
      renderer.setStyle(el.nativeElement, 'cursor', 'pointer');
    });
    renderer.listen(el.nativeElement, 'mouseleave', () => {
      renderer.setStyle(el.nativeElement, 'background-color', '#f26a31');
      renderer.setStyle(el.nativeElement, 'cursor', 'pointer');
    });

    renderer.listen(el.nativeElement, 'mousedown', () => {
      renderer.setStyle(el.nativeElement, 'background-color', '#db4b0f');
      renderer.setStyle(el.nativeElement, 'box-shadow', '0px 0px 10px rgba(0,0,0,0.35)');
    });
    renderer.listen(el.nativeElement, 'mouseup', () => {
      renderer.setStyle(el.nativeElement, 'background-color', '#eb906a');
      renderer.setStyle(el.nativeElement, 'box-shadow', 'none');
    });
}

export function setPrimaryButton(el: ElementRef, renderer: Renderer2){
    renderer.setStyle(el.nativeElement, 'background-color', 'transparent');
    renderer.setStyle(el.nativeElement, 'color', '#f26a31');
    renderer.setStyle(el.nativeElement, 'font-weight', 'bold');
    renderer.setStyle(el.nativeElement, 'border', '2px solid #f26a31');
    renderer.setStyle(el.nativeElement, 'border-radius', '4px');
    renderer.setStyle(el.nativeElement, 'padding', '0.75rem 2rem');
    renderer.setStyle(el.nativeElement, 'font-family', 'Nunito Sans');
    renderer.setStyle(el.nativeElement, 'transition', '0.2s');

    renderer.listen(el.nativeElement, 'mouseenter', () => {
      renderer.setStyle(el.nativeElement, 'background-color', '#fff1eb');
      renderer.setStyle(el.nativeElement, 'cursor', 'pointer');
    });
    renderer.listen(el.nativeElement, 'mouseleave', () => {
      renderer.setStyle(el.nativeElement, 'background-color', 'transparent');
      renderer.setStyle(el.nativeElement, 'cursor', 'pointer');
    });

    renderer.listen(el.nativeElement, 'mousedown', () => {
      renderer.setStyle(el.nativeElement, 'background-color', '#fadacd');
      renderer.setStyle(el.nativeElement, 'box-shadow', '0px 0px 10px rgba(0,0,0,0.2)');
    });
    renderer.listen(el.nativeElement, 'mouseup', () => {
      renderer.setStyle(el.nativeElement, 'background-color', '#fff1eb');
      renderer.setStyle(el.nativeElement, 'box-shadow', 'none');
    });
}

export function setWarningButton(el: ElementRef, renderer: Renderer2){
    renderer.setStyle(el.nativeElement, 'background-color', '#d10202');
    renderer.setStyle(el.nativeElement, 'color', '#ffffff');
    renderer.setStyle(el.nativeElement, 'border', 'none');
    renderer.setStyle(el.nativeElement, 'border-radius', '4px');
    renderer.setStyle(el.nativeElement, 'font-weight', 'bold');
    renderer.setStyle(el.nativeElement, 'padding', '0.75rem 2rem');
    renderer.setStyle(el.nativeElement, 'font-family', 'Nunito Sans');
    renderer.setStyle(el.nativeElement, 'transition', '0.2s');

    renderer.listen(el.nativeElement, 'mouseenter', () => {
      renderer.setStyle(el.nativeElement, 'background-color', '#ff5252');
      renderer.setStyle(el.nativeElement, 'cursor', 'pointer');
    });
    renderer.listen(el.nativeElement, 'mouseleave', () => {
      renderer.setStyle(el.nativeElement, 'background-color', '#d10202');
      renderer.setStyle(el.nativeElement, 'cursor', 'pointer');
    });

    renderer.listen(el.nativeElement, 'mousedown', () => {
      renderer.setStyle(el.nativeElement, 'background-color', '#a70000');
      renderer.setStyle(el.nativeElement, 'box-shadow', '0px 0px 10px rgba(255,0,0,0.7)');
    });
    renderer.listen(el.nativeElement, 'mouseup', () => {
      renderer.setStyle(el.nativeElement, 'background-color', '#d10202');
      renderer.setStyle(el.nativeElement, 'box-shadow', 'none');
    });
}
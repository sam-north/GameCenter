import { Component, Input } from '@angular/core';

@Component({
    // selector is name of the component as it will be used in html
  selector: 'square-component',
  template: `
  <button>
  {{ value }}
  </button>
  `,
  styles: ['button {border: 1px gray solid; height: 200px; width: 200px; font-size: 115px}']
})
export class SquareComponent {
    @Input() value: 'X' | 'O'
}

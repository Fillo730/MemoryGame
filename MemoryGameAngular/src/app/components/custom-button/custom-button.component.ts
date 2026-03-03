//AngularCore
import { Component, EventEmitter, Input, Output } from '@angular/core';

@Component({
  selector: 'custom-button-component',
  imports: [],
  templateUrl: './custom-button.component.html',
  styleUrl: './custom-button.component.css',
})
export class CustomButtonComponent {
  @Input() text : string = "";
  @Input() disabled : boolean = false;

  @Output() onClick = new EventEmitter<void>();

  public handleClick(): void {
    this.onClick.emit();
  }
}

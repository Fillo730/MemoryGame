import { Component, EventEmitter, Input, Output } from '@angular/core';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'generic-button-component',
  imports: [RouterLink],
  templateUrl: './generic-button.component.html',
  styleUrl: './generic-button.component.css',
})
export class GenericButtonComponent {
  @Input() label!: string;
  @Input() routerLink : string | null = null;
  @Input() disabled : boolean = false;

  @Output() onClick = new EventEmitter<void>();

  public handleClick() : void {
    this.onClick.emit();
  }
}

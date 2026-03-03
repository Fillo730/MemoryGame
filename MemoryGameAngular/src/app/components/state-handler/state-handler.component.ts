//Angular
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';

//Components
import { GenericButtonComponent } from '../generic-button/generic-button.component';

//Models
import { StateHandlerType } from '../../models/components/StateHandlerType.model';

@Component({
  selector: 'state-handler-component',
  imports: [CommonModule, GenericButtonComponent],
  templateUrl: './state-handler.component.html',
  styleUrl: './state-handler.component.css',
})

export class StateHandlerComponent {
  @Input({ required: true }) type!: StateHandlerType;
  @Input() title!: string;
  @Input() text!: string;
  @Input() buttonLabel !: string;
  @Input() icon?: string;

  @Output() onClick = new EventEmitter<void>();

  public handleClik() : void {
    this.onClick.emit();
  }

  public getDefaultIcon(): string {
    if (this.icon) return this.icon;
    switch (this.type) {
      case 'loading': return 'sync';
      case 'noResult': return 'search_off';
      case 'error': return 'error';
      default: return 'help';
    }
  }
}
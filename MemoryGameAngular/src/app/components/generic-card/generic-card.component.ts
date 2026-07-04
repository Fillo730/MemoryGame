import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'generic-card-component',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './generic-card.component.html',
  styleUrl: './generic-card.component.css',
})
export class GenericCardComponent {
  @Input() frontText: string = '';
  @Input() backText: string = '';
  @Input() backTextSmaller : boolean = false;

  @Output() cardClick = new EventEmitter<void>();

  public handleClick(): void {
    this.cardClick.emit();
  }
}
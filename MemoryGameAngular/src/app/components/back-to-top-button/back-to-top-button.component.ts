//Angular
import { Component } from '@angular/core';

//Helpers
import { scrollToTop } from '../../helpers/scrollFunctions.helper';

@Component({
  selector: 'back-to-top-button-component',
  imports: [],
  templateUrl: './back-to-top-button.component.html',
  styleUrl: './back-to-top-button.component.css',
})

export class BackToTopButton {
  public handleClick() : void {
    scrollToTop();
  }
}

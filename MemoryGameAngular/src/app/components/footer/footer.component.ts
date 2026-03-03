//Angular
import { Component } from '@angular/core';

//Components
import { BackToTopButton } from '../back-to-top-button/back-to-top-button.component';

@Component({
  selector: 'footer-component',
  imports: [BackToTopButton],
  templateUrl: './footer.component.html',
  styleUrl: './footer.component.css',
})
export class FooterComponent {

}

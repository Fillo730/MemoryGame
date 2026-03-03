//Angular
import { Component, inject, Input } from '@angular/core';

//Services
import { NavigationService } from '../../services/NavigationService.service';

//Helpers
import { LINKEDIN_URL } from '../../helpers/urls.helper';

@Component({
  selector: 'linkedin-button-component',
  imports: [],
  templateUrl: './linkedin-button.component.html',
  styleUrl: './linkedin-button.component.css',
})

export class LinkedinButtonComponent {
  @Input() buttonLabel !: string;

  private navigationService = inject(NavigationService);

  handleClick() : void {
    this.navigationService.goToGenericUrl(LINKEDIN_URL);
  }
}

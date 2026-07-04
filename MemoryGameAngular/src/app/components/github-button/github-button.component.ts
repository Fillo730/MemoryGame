//Angular
import { Component, inject, Input } from '@angular/core';

//Services
import { NavigationService } from '../../services/NavigationService.service';

//Helpers
import { GITHUB_URL } from '../../helpers/urls.helper';

@Component({
  selector: 'github-button-component',
  imports: [],
  templateUrl: './github-button.component.html',
  styleUrl: './github-button.component.css',
})

export class GithubButtonComponent {
  @Input() buttonLabel !: string;

  private navigationService = inject(NavigationService);

  handleClick() : void {
    this.navigationService.goToGenericUrl(GITHUB_URL);  
  }
}

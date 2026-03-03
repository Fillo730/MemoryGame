//Angular
import { Component, inject } from '@angular/core';

//Components
import { LanguagePickerComponent } from '../language-picker/language-picker.component';
import { GenericButtonComponent } from '../generic-button/generic-button.component';

//Services
import { RouterLink, RouterLinkActive } from '@angular/router';
import { NavigationService, AppRoutes} from '../../services/NavigationService.service';

//i18n
import { TranslateModule} from '@ngx-translate/core';

//Services
import { ThemeService } from '../../services/theme-service.service';
import { AuthService } from '../../services/auth-service.service';

@Component({
  selector: 'header-component',
  standalone: true,
  imports: [TranslateModule, LanguagePickerComponent, RouterLink, RouterLinkActive,GenericButtonComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent {
  public themeService = inject(ThemeService);
  public navigationService = inject(NavigationService);
  private authService = inject(AuthService)

  public routes = AppRoutes;
  
  public isLoggedIn() : boolean {
    return this.authService.isLoggedIn();
  }

  public getUsername() : string {
    return this.authService.currentUser()?.username ?? '';
  }
}

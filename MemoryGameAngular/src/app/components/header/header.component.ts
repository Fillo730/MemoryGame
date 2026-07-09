//Angular
import { Component, effect, inject, OnDestroy } from '@angular/core';

//Components
import { LanguagePickerComponent } from '../language-picker/language-picker.component';
import { GenericButtonComponent } from '../generic-button/generic-button.component';

//Services
import { RouterLink, RouterLinkActive } from '@angular/router';
import { NavigationService, AppRoutes} from '../../services/NavigationService.service';
import { ThemeService } from '../../services/theme-service.service';
import { AuthService } from '../../services/auth-service.service';
import { FriendsService } from '../../services/friends-service.service';

//i18n
import { TranslateModule} from '@ngx-translate/core';

//Models
import { THEMES } from '../../models/types/Theme.model';

const PENDING_REQUESTS_POLL_INTERVAL_MS = 30000;

@Component({
  selector: 'header-component',
  standalone: true,
  imports: [TranslateModule, LanguagePickerComponent, RouterLink, RouterLinkActive,GenericButtonComponent],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css',
})
export class HeaderComponent implements OnDestroy {
  public themeService = inject(ThemeService);
  public navigationService = inject(NavigationService);
  private authService = inject(AuthService)
  public friendsService = inject(FriendsService);

  public routes = AppRoutes;

  public themes = THEMES;

  private pollHandle: ReturnType<typeof setInterval> | null = null;

  constructor() {
    effect(() => {
      if (this.isLoggedIn()) {
        this.friendsService.refreshPendingRequestsCount();
        this.startPolling();
      } else {
        this.stopPolling();
        this.friendsService.pendingRequestsCount.set(0);
      }
    });
  }

  ngOnDestroy(): void {
    this.stopPolling();
  }

  public isLoggedIn() : boolean {
    return this.authService.isLoggedIn();
  }

  public getUsername() : string {
    return this.authService.currentUser()?.username ?? '';
  }

  public handleLogout() : void {
    this.authService.logout();
    this.navigationService.goToHome();
  }

  private startPolling(): void {
    if (this.pollHandle !== null) return;

    this.pollHandle = setInterval(() => {
      this.friendsService.refreshPendingRequestsCount();
    }, PENDING_REQUESTS_POLL_INTERVAL_MS);
  }

  private stopPolling(): void {
    if (this.pollHandle !== null) {
      clearInterval(this.pollHandle);
      this.pollHandle = null;
    }
  }
}

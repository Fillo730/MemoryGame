//Angular
import { Component, computed, effect, inject, signal } from '@angular/core';

//Components
import { HeaderComponent } from '../../components/header/header.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { GenericButtonComponent } from '../../components/generic-button/generic-button.component';
import { StateHandlerComponent } from '../../components/state-handler/state-handler.component';
import { UpdateProfileComponent } from '../../components/update-profile/update-profile.component';

//pipe
import { DecimalPipe } from '@angular/common';

//i18n
import { TranslatePipe } from '@ngx-translate/core';

//Services
import { AuthService } from '../../services/auth-service.service';
import { GameResultsService } from '../../services/gameResults-service.service';
import { NavigationService } from '../../services/NavigationService.service';
import { UsersService } from '../../services/users-service.service';
import { LanguageService } from '../../services/language-service.service';

//rxjs
import { finalize } from 'rxjs';

//Models
import { UserStats } from '../../models/stats/userStats.dto';
import { UpdateProfile } from '../../models/components/UpdateProfile.model';
import { UpdateRequest } from '../../models/requests/UpdateRequest.model';

@Component({
  selector: 'profile.page',
  imports: [TranslatePipe, HeaderComponent, FooterComponent, GenericButtonComponent, StateHandlerComponent, DecimalPipe, UpdateProfileComponent],
  templateUrl: './profile.page.html',
  styleUrl: './profile.page.css',
})
export class ProfilePage {
  private authService = inject(AuthService);
  private gameResultsService = inject(GameResultsService);
  private navigationService = inject(NavigationService);
  private usersService = inject(UsersService);
  private languageService = inject(LanguageService);

  public currentUser = computed(() => this.authService.currentUser()!);
  public userData = computed<UpdateProfile>(() => ({
    username: this.currentUser().username,
    email: this.currentUser().email
  }))
  public initials = computed(() => this.currentUser().username.slice(0, 2).toUpperCase());
  public playedStats = computed(() => (this.userStats() ?? []).filter(stat => stat.gamesPlayed > 0));
  public hasPlayedAnyGame = computed(() => this.playedStats().length > 0);

  public isLoading = signal<boolean>(false);
  public error = signal<string | null>(null);
  public userStats = signal<UserStats[] | null>(null);
  public modalOpen = signal<boolean>(false);

  constructor() {
    effect(() => {
      this.languageService.language();
      this.loadData();
    })
  }

  public loadData(): void {
    this.isLoading.set(true);
    this.error.set(null);

    this.gameResultsService.getUserStats().pipe(
      finalize(() => this.isLoading.set(false))
    ).subscribe({
      next: (response) => {
        if (response.success) {
          this.userStats.set(response.data);
        } else {
          this.error.set(response.message || 'Errore nel caricamento dati');
        }
      },
      error: () => {
        this.error.set('Impossibile connettersi al server.');
      }
    });
  }

  public handleEditProfile() : void {
    this.modalOpen.set(true);
  }

  public handleLogout() : void {
    this.authService.logout();
    this.navigationService.goToHome();
  }

  public handleGoToPlay() : void {
    this.navigationService.goToPlay();
  }

  public handleSaveProfile(updateRequest : UpdateRequest) : void {
    this.modalOpen.set(false);
    this.usersService.updateUser(updateRequest).subscribe();
  }

  public handleCloseModal() : void {
    this.modalOpen.set(false);
  }
}

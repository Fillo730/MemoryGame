//Angular
import { Component, computed, effect, inject, signal } from '@angular/core';

//Components
import { HeaderComponent } from '../../components/header/header.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { GenericButtonComponent } from '../../components/generic-button/generic-button.component';
import { StateHandlerComponent } from '../../components/state-handler/state-handler.component';
import { UpdateProfileComponent } from '../../components/update-profile/update-profile.component';

//pipe
import { DecimalPipe, DatePipe } from '@angular/common';

//i18n
import { TranslatePipe } from '@ngx-translate/core';

//Services
import { AuthService } from '../../services/auth-service.service';
import { GameResultsService } from '../../services/gameResults-service.service';
import { NavigationService } from '../../services/NavigationService.service';
import { UsersService } from '../../services/users-service.service';
import { LanguageService } from '../../services/language-service.service';
import { AchievementsService } from '../../services/achievements-service.service';

//rxjs
import { finalize } from 'rxjs';

//Models
import { UserStats } from '../../models/stats/userStats.dto';
import { UpdateProfile } from '../../models/components/UpdateProfile.model';
import { UpdateRequest } from '../../models/requests/UpdateRequest.model';
import { Achievement } from '../../models/entitiesDto/Achievement.model';

@Component({
  selector: 'profile.page',
  imports: [TranslatePipe, HeaderComponent, FooterComponent, GenericButtonComponent, StateHandlerComponent, DecimalPipe, DatePipe, UpdateProfileComponent],
  templateUrl: './profile.page.html',
  styleUrl: './profile.page.css',
})
export class ProfilePage {
  private authService = inject(AuthService);
  private gameResultsService = inject(GameResultsService);
  private navigationService = inject(NavigationService);
  private usersService = inject(UsersService);
  private languageService = inject(LanguageService);
  private achievementsService = inject(AchievementsService);

  public currentUser = computed(() => this.authService.currentUser()!);
  public userData = computed<UpdateProfile>(() => ({
    username: this.currentUser().username,
    email: this.currentUser().email
  }))
  public initials = computed(() => this.currentUser().username.slice(0, 2).toUpperCase());
  public playedStats = computed(() => (this.userStats() ?? []).filter(stat => stat.gamesPlayed > 0));
  public hasPlayedAnyGame = computed(() => this.playedStats().length > 0);
  public totalGamesPlayed = computed(() => this.playedStats().reduce((sum, stat) => sum + stat.gamesPlayed, 0));
  public favoriteDifficulty = computed(() => {
    const stats = this.playedStats();

    if (stats.length === 0) return null;

    return stats.reduce((max, stat) => stat.gamesPlayed > max.gamesPlayed ? stat : max).difficulty;
  });

  public sortedAchievements = computed(() => {
    const achievements = this.achievements() ?? [];

    return [...achievements].sort((a, b) => Number(b.unlocked) - Number(a.unlocked));
  });
  public unlockedAchievementsCount = computed(() => (this.achievements() ?? []).filter(a => a.unlocked).length);
  public totalAchievementsCount = computed(() => (this.achievements() ?? []).length);
  public achievementsProgress = computed(() => {
    const total = this.totalAchievementsCount();

    return total === 0 ? 0 : Math.round((this.unlockedAchievementsCount() / total) * 100);
  });

  public isLoading = signal<boolean>(false);
  public error = signal<string | null>(null);
  public userStats = signal<UserStats[] | null>(null);
  public achievements = signal<Achievement[] | null>(null);
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

    this.achievementsService.getAchievements().subscribe({
      next: (response) => {
        if (response.success) {
          this.achievements.set(response.data);
        }
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

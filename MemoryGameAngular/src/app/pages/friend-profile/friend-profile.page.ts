//Angular
import { Component, computed, effect, inject, signal } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

//pipe
import { DecimalPipe, DatePipe } from '@angular/common';

//Components
import { HeaderComponent } from '../../components/header/header.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { GenericButtonComponent } from '../../components/generic-button/generic-button.component';
import { StateHandlerComponent } from '../../components/state-handler/state-handler.component';

//i18n
import { TranslatePipe } from '@ngx-translate/core';

//Services
import { FriendsService } from '../../services/friends-service.service';
import { NavigationService } from '../../services/NavigationService.service';
import { LanguageService } from '../../services/language-service.service';

//Models
import { FriendProfile } from '../../models/entitiesDto/FriendProfile.model';

//Helpers
import { formatDuration } from '../../helpers/timeFunctions.helper';

@Component({
  selector: 'friend-profile.page',
  imports: [TranslatePipe, HeaderComponent, FooterComponent, GenericButtonComponent, StateHandlerComponent, DecimalPipe, DatePipe],
  templateUrl: './friend-profile.page.html',
  styleUrl: './friend-profile.page.css',
})
export class FriendProfilePage {
  private route = inject(ActivatedRoute);
  private friendsService = inject(FriendsService);
  private navigationService = inject(NavigationService);
  private languageService = inject(LanguageService);

  public readonly historyPageSize = 8;
  public readonly achievementsPageSize = 6;

  public userId = signal<number>(0);
  public profile = signal<FriendProfile | null>(null);
  public isLoading = signal<boolean>(false);
  public error = signal<string | null>(null);
  public achievementsPage = signal<number>(1);

  public initials = computed(() => (this.profile()?.username ?? '').slice(0, 2).toUpperCase());
  public playedStats = computed(() => (this.profile()?.stats ?? []).filter(stat => stat.gamesPlayed > 0));
  public hasPlayedAnyGame = computed(() => this.playedStats().length > 0);

  public sortedAchievements = computed(() => {
    const achievements = this.profile()?.achievements ?? [];
    return [...achievements].sort((a, b) => Number(b.unlocked) - Number(a.unlocked));
  });
  public unlockedAchievementsCount = computed(() => (this.profile()?.achievements ?? []).filter(a => a.unlocked).length);
  public totalAchievementsCount = computed(() => (this.profile()?.achievements ?? []).length);
  public achievementsProgress = computed(() => {
    const total = this.totalAchievementsCount();
    return total === 0 ? 0 : Math.round((this.unlockedAchievementsCount() / total) * 100);
  });
  public totalAchievementsPages = computed(() =>
    Math.max(1, Math.ceil(this.sortedAchievements().length / this.achievementsPageSize))
  );
  public pagedAchievements = computed(() => {
    const start = (this.achievementsPage() - 1) * this.achievementsPageSize;
    return this.sortedAchievements().slice(start, start + this.achievementsPageSize);
  });
  public achievementsPageNumbers = computed(() =>
    Array.from({ length: this.totalAchievementsPages() }, (_, i) => i + 1)
  );

  public history = computed(() => this.profile()?.history?.items ?? []);
  public historyTotalPages = computed(() => {
    const history = this.profile()?.history;
    if (!history) return 1;
    return Math.max(1, Math.ceil(history.totalCount / history.pageSize));
  });
  public historyPage = computed(() => this.profile()?.history?.page ?? 1);
  public historyPageNumbers = computed(() =>
    Array.from({ length: this.historyTotalPages() }, (_, i) => i + 1)
  );

  constructor() {
    effect(() => {
      this.languageService.language();
      const idParam = this.route.snapshot.paramMap.get('userId');

      if (idParam) {
        this.userId.set(Number(idParam));
        this.loadProfile(1);
      }
    });
  }

  public loadProfile(historyPage: number): void {
    this.isLoading.set(true);
    this.error.set(null);

    this.friendsService.getFriendProfile(this.userId(), historyPage, this.historyPageSize).subscribe({
      next: (response) => {
        this.isLoading.set(false);

        if (response.success) {
          this.profile.set(response.data);
          this.achievementsPage.set(1);
        } else {
          this.error.set(response.message || 'Errore nel caricamento dati');
        }
      },
      error: () => {
        this.isLoading.set(false);
        this.error.set('Impossibile connettersi al server.');
      }
    });
  }

  public handleAchievementsPrevPage(): void {
    this.achievementsPage.update(page => Math.max(1, page - 1));
  }

  public handleAchievementsNextPage(): void {
    this.achievementsPage.update(page => Math.min(this.totalAchievementsPages(), page + 1));
  }

  public handleAchievementsGoToPage(page: number): void {
    this.achievementsPage.set(page);
  }

  public handleHistoryPrevPage(): void {
    if (this.historyPage() > 1) {
      this.loadProfile(this.historyPage() - 1);
    }
  }

  public handleHistoryNextPage(): void {
    if (this.historyPage() < this.historyTotalPages()) {
      this.loadProfile(this.historyPage() + 1);
    }
  }

  public handleHistoryGoToPage(page: number): void {
    this.loadProfile(page);
  }

  public handleGoBack(): void {
    this.navigationService.goToProfile();
  }

  public formatTime(totalSeconds: number): string {
    return formatDuration(totalSeconds);
  }
}

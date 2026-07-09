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
import { FriendsService } from '../../services/friends-service.service';

//rxjs
import { finalize } from 'rxjs';

//Models
import { UserStats } from '../../models/stats/userStats.dto';
import { UpdateProfile } from '../../models/components/UpdateProfile.model';
import { UpdateRequest } from '../../models/requests/UpdateRequest.model';
import { Achievement } from '../../models/entitiesDto/Achievement.model';
import { GameResult } from '../../models/entitiesDto/GameResult.model';
import { Friend } from '../../models/entitiesDto/Friend.model';
import { FriendRequest } from '../../models/entitiesDto/FriendRequest.model';
import { UserSearchResult } from '../../models/entitiesDto/UserSearchResult.model';

//Helpers
import { formatDuration } from '../../helpers/timeFunctions.helper';

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
  private friendsService = inject(FriendsService);

  public currentUser = computed(() => this.authService.currentUser()!);
  public userData = computed<UpdateProfile>(() => ({
    username: this.currentUser().username,
    email: this.currentUser().email,
    bio: this.currentUser().bio,
    country: this.currentUser().country,
    birthDate: (this.currentUser().birthDate as unknown as string | null)?.slice(0, 10) ?? null
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

  public readonly achievementsPageSize = 6;
  public achievementsPage = signal<number>(1);
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

  public readonly gameHistoryPageSize = 8;
  public gameHistory = signal<GameResult[] | null>(null);
  public gameHistoryPage = signal<number>(1);
  public gameHistoryTotalCount = signal<number>(0);
  public isGameHistoryLoading = signal<boolean>(false);

  public hasGameHistory = computed(() => this.gameHistoryTotalCount() > 0);
  public gameHistoryTotalPages = computed(() =>
    Math.max(1, Math.ceil(this.gameHistoryTotalCount() / this.gameHistoryPageSize))
  );
  public gameHistoryPageNumbers = computed(() =>
    Array.from({ length: this.gameHistoryTotalPages() }, (_, i) => i + 1)
  );

  public friends = signal<Friend[] | null>(null);
  public incomingRequests = signal<FriendRequest[] | null>(null);
  public searchResults = signal<UserSearchResult[] | null>(null);
  public searchQuery = signal<string>('');
  public isSearching = signal<boolean>(false);
  public isFriendsLoading = signal<boolean>(false);

  public hasSearched = computed(() => this.searchQuery().trim().length >= 2);
  public hasFriends = computed(() => (this.friends() ?? []).length > 0);
  public hasIncomingRequests = computed(() => (this.incomingRequests() ?? []).length > 0);

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
          this.achievementsPage.set(1);
        }
      }
    });

    this.loadGameHistory(1);
    this.loadFriendsData();
  }

  public loadFriendsData(): void {
    this.isFriendsLoading.set(true);

    this.friendsService.getFriends().subscribe({
      next: (response) => {
        this.isFriendsLoading.set(false);
        if (response.success) {
          this.friends.set(response.data);
        }
      },
      error: () => this.isFriendsLoading.set(false)
    });

    this.friendsService.getIncomingRequests().subscribe({
      next: (response) => {
        if (response.success) {
          this.incomingRequests.set(response.data);
          this.friendsService.pendingRequestsCount.set(response.data.length);
        }
      }
    });
  }

  public handleFriendSearchInput(query: string): void {
    this.searchQuery.set(query);

    if (query.trim().length < 2) {
      this.searchResults.set(null);
      return;
    }

    this.isSearching.set(true);

    this.friendsService.searchUsers(query.trim()).subscribe({
      next: (response) => {
        this.isSearching.set(false);
        if (response.success) {
          this.searchResults.set(response.data);
        }
      },
      error: () => this.isSearching.set(false)
    });
  }

  public handleSendFriendRequest(userId: number): void {
    this.friendsService.sendFriendRequest(userId).subscribe({
      next: (response) => {
        if (response.success) {
          this.searchResults.update(results =>
            (results ?? []).map(r => r.userId === userId ? { ...r, status: 'PendingOutgoing' as const } : r)
          );
        }
      }
    });
  }

  public handleAcceptFriendRequest(friendshipId: number): void {
    this.friendsService.acceptRequest(friendshipId).subscribe({
      next: (response) => {
        if (response.success) {
          this.loadFriendsData();
        }
      }
    });
  }

  public handleDeclineFriendRequest(friendshipId: number): void {
    this.friendsService.declineRequest(friendshipId).subscribe({
      next: (response) => {
        if (response.success) {
          this.incomingRequests.update(requests => (requests ?? []).filter(r => r.friendshipId !== friendshipId));
          this.friendsService.pendingRequestsCount.update(count => Math.max(0, count - 1));
        }
      }
    });
  }

  public handleRemoveFriend(friendshipId: number): void {
    this.friendsService.removeFriend(friendshipId).subscribe({
      next: (response) => {
        if (response.success) {
          this.friends.update(friends => (friends ?? []).filter(f => f.friendshipId !== friendshipId));
        }
      }
    });
  }

  public handleViewFriendProfile(userId: number): void {
    this.navigationService.goToFriendProfile(userId);
  }

  public loadGameHistory(page: number): void {
    this.isGameHistoryLoading.set(true);

    this.gameResultsService.getGameHistory(page, this.gameHistoryPageSize).pipe(
      finalize(() => this.isGameHistoryLoading.set(false))
    ).subscribe({
      next: (response) => {
        if (response.success) {
          this.gameHistory.set(response.data.items);
          this.gameHistoryTotalCount.set(response.data.totalCount);
          this.gameHistoryPage.set(response.data.page);
        }
      }
    });
  }

  public handleGameHistoryPrevPage(): void {
    if (this.gameHistoryPage() > 1) {
      this.loadGameHistory(this.gameHistoryPage() - 1);
    }
  }

  public handleGameHistoryNextPage(): void {
    if (this.gameHistoryPage() < this.gameHistoryTotalPages()) {
      this.loadGameHistory(this.gameHistoryPage() + 1);
    }
  }

  public handleGameHistoryGoToPage(page: number): void {
    this.loadGameHistory(page);
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

  public formatTime(totalSeconds: number): string {
    return formatDuration(totalSeconds);
  }
}

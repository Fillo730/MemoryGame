//Angular
import { Component, computed, inject, signal } from '@angular/core';
import { DecimalPipe } from '@angular/common';

//Components
import { HeaderComponent } from '../../components/header/header.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { CustomButtonComponent } from '../../components/custom-button/custom-button.component';
import { NatureGalleryComponent } from '../../components/nature-gallery/nature-gallery.component';

//Services
import { NavigationService } from '../../services/NavigationService.service';
import { AuthService } from '../../services/auth-service.service';
import { LeaderboardService } from '../../services/leaderboard-service.service';
import { FriendsService } from '../../services/friends-service.service';

//Models
import { PlatformStats } from '../../models/entitiesDto/PlatformStats.model';
import { FriendComparisonEntry } from '../../models/entitiesDto/Friend.model';

//public
import { defaultImages } from '../../../../public/DefaultImages';

//i18n
import { TranslateModule } from '@ngx-translate/core';

@Component({
  selector: 'home-page',
  imports: [HeaderComponent, FooterComponent, CustomButtonComponent, NatureGalleryComponent, TranslateModule, DecimalPipe],
  templateUrl: './home.page.html',
  styleUrl: './home.page.css',
})

export class HomePage {
  public navigationService = inject(NavigationService);
  public authService = inject(AuthService);
  private leaderboardService = inject(LeaderboardService);
  private friendsService = inject(FriendsService);

  public defaultImages: string[] = defaultImages;
  public heroCardIcons: string[] = ['eco', 'forest', 'park', 'local_florist', 'spa', 'water_drop'];

  public platformStats = signal<PlatformStats | null>(null);
  public friendsComparison = signal<FriendComparisonEntry[] | null>(null);
  public isComparisonLoading = signal<boolean>(false);
  public communityStatIndex = signal(0);

  public hasFriendsComparison = computed(() => (this.friendsComparison() ?? []).length > 1);

  public communityStatsItems = computed<{ icon: string; value: string | number; isNumber: boolean; labelKey: string }[]>(() => {
    const stats = this.platformStats();
    if (!stats) {
      return [];
    }

    const items: { icon: string; value: string | number; isNumber: boolean; labelKey: string }[] = [
      { icon: 'group', value: stats.totalPlayers, isNumber: true, labelKey: 'Home.CommunityPlayers' },
      { icon: 'sports_esports', value: stats.totalGamesPlayed, isNumber: true, labelKey: 'Home.CommunityGamesPlayed' },
    ];

    if (stats.mostPopularDifficulty) {
      items.push({ icon: 'local_fire_department', value: stats.mostPopularDifficulty.label, isNumber: false, labelKey: 'Home.CommunityPopularDifficulty' });
    }

    return items;
  });

  public goToCommunityStat(index: number): void {
    const max = this.communityStatsItems().length - 1;
    this.communityStatIndex.set(Math.max(0, Math.min(index, max)));
  }

  public prevCommunityStat(): void {
    this.goToCommunityStat(this.communityStatIndex() - 1);
  }

  public nextCommunityStat(): void {
    this.goToCommunityStat(this.communityStatIndex() + 1);
  }

  constructor() {
    this.leaderboardService.getPlatformStats().subscribe({
      next: (response) => {
        if (response.success) {
          this.platformStats.set(response.data);
        }
      }
    });

    if (this.authService.isLoggedIn()) {
      this.isComparisonLoading.set(true);

      this.friendsService.getFriendsComparison().subscribe({
        next: (response) => {
          this.isComparisonLoading.set(false);
          if (response.success) {
            this.friendsComparison.set(response.data);
          }
        },
        error: () => this.isComparisonLoading.set(false)
      });
    }
  }
}

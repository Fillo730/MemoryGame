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

  public hasFriendsComparison = computed(() => (this.friendsComparison() ?? []).length > 1);

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

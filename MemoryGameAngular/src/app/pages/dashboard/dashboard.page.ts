//Angular
import { Component, computed, effect, inject, signal } from '@angular/core';

//Components
import { HeaderComponent } from '../../components/header/header.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { StateHandlerComponent } from '../../components/state-handler/state-handler.component';

//Charts
import { BaseChartDirective } from 'ng2-charts';
import { ChartConfiguration, ChartData } from 'chart.js';

//i18n
import { TranslatePipe } from '@ngx-translate/core';

//Services
import { LeaderboardService } from '../../services/leaderboard-service.service';
import { LanguageService } from '../../services/language-service.service';
import { ThemeService } from '../../services/theme-service.service';
import { NavigationService } from '../../services/NavigationService.service';

//rxjs
import { finalize } from 'rxjs';

//Models
import { Leaderboard, DifficultyBestScores, DifficultyBestTimes } from '../../models/leaderboard/Leaderboard.model';
import { THEMES } from '../../models/types/Theme.model';

//Helpers
import { formatDuration } from '../../helpers/timeFunctions.helper';

@Component({
  selector: 'dashboard.page',
  imports: [TranslatePipe, HeaderComponent, FooterComponent, StateHandlerComponent, BaseChartDirective],
  templateUrl: './dashboard.page.html',
  styleUrl: './dashboard.page.css',
})
export class DashboardPage {
  private leaderboardService = inject(LeaderboardService);
  private languageService = inject(LanguageService);
  private navigationService = inject(NavigationService);
  public themeService = inject(ThemeService);

  public isLoading = signal(false);
  public hasError = signal(false);
  public leaderboard = signal<Leaderboard | null>(null);

  public hasData = computed(() => (this.leaderboard()?.topPlayers.length ?? 0) > 0);
  public topThreePlayers = computed(() => this.leaderboard()?.topPlayers.slice(0, 3) ?? []);
  public restPlayers = computed(() => this.leaderboard()?.topPlayers.slice(3) ?? []);

  public chartData = computed<ChartData<'bar'>>(() => {
    const gamesPerDifficulty = this.leaderboard()?.gamesPerDifficulty ?? [];
    const barColor = this.themeService.theme() === THEMES.DARK ? '#00bcd4' : '#1e3a8a';

    return {
      labels: gamesPerDifficulty.map(item => item.difficulty.label),
      datasets: [{
        data: gamesPerDifficulty.map(item => item.gamesPlayed),
        backgroundColor: barColor,
        hoverBackgroundColor: barColor,
        maxBarThickness: 24,
        borderRadius: { topLeft: 0, bottomLeft: 0, topRight: 4, bottomRight: 4 },
        borderSkipped: false,
      }],
    };
  });

  public chartOptions = computed<ChartConfiguration<'bar'>['options']>(() => {
    const isDark = this.themeService.theme() === THEMES.DARK;
    const textColor = isDark ? '#ffffff' : '#0f172a';
    const gridColor = isDark ? 'rgba(255, 255, 255, 0.08)' : 'rgba(15, 23, 42, 0.08)';
    const surfaceColor = isDark ? '#1e293b' : '#ffffff';

    return {
      indexAxis: 'y',
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        legend: { display: false },
        tooltip: {
          backgroundColor: surfaceColor,
          titleColor: textColor,
          bodyColor: textColor,
          borderColor: gridColor,
          borderWidth: 1,
        },
      },
      scales: {
        x: {
          beginAtZero: true,
          ticks: { color: textColor, precision: 0 },
          grid: { color: gridColor },
        },
        y: {
          ticks: { color: textColor },
          grid: { display: false },
        },
      },
    };
  });

  private static readonly RANK_COLORS = ['#f5c518', '#c0c0c0', '#cd7f32'];

  constructor() {
    effect(() => {
      this.languageService.language();
      this.loadLeaderboard();
    });
  }

  public handleGoToPlay(): void {
    this.navigationService.goToPlay();
  }

  public formatTime(totalSeconds: number): string {
    return formatDuration(totalSeconds);
  }

  private getRankedBarColor(index: number): string {
    const barColor = this.themeService.theme() === THEMES.DARK ? '#00bcd4' : '#1e3a8a';
    return DashboardPage.RANK_COLORS[index] ?? barColor;
  }

  public getScoreChartData(entry: DifficultyBestScores): ChartData<'bar'> {
    return {
      labels: entry.topScores.map(s => s.username),
      datasets: [{
        data: entry.topScores.map(s => s.moves),
        backgroundColor: entry.topScores.map((_, i) => this.getRankedBarColor(i)),
        maxBarThickness: 22,
        borderRadius: { topLeft: 0, bottomLeft: 0, topRight: 4, bottomRight: 4 },
        borderSkipped: false,
      }],
    };
  }

  public getTimeChartData(entry: DifficultyBestTimes): ChartData<'bar'> {
    return {
      labels: entry.topTimes.map(t => t.username),
      datasets: [{
        data: entry.topTimes.map(t => t.durationSeconds),
        backgroundColor: entry.topTimes.map((_, i) => this.getRankedBarColor(i)),
        maxBarThickness: 22,
        borderRadius: { topLeft: 0, bottomLeft: 0, topRight: 4, bottomRight: 4 },
        borderSkipped: false,
      }],
    };
  }

  public rankedChartOptions = computed<ChartConfiguration<'bar'>['options']>(() => {
    const isDark = this.themeService.theme() === THEMES.DARK;
    const textColor = isDark ? '#ffffff' : '#0f172a';
    const gridColor = isDark ? 'rgba(255, 255, 255, 0.08)' : 'rgba(15, 23, 42, 0.08)';
    const surfaceColor = isDark ? '#1e293b' : '#ffffff';

    return {
      indexAxis: 'y',
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        legend: { display: false },
        tooltip: {
          backgroundColor: surfaceColor,
          titleColor: textColor,
          bodyColor: textColor,
          borderColor: gridColor,
          borderWidth: 1,
        },
      },
      scales: {
        x: {
          beginAtZero: true,
          ticks: { color: textColor, precision: 0 },
          grid: { color: gridColor },
        },
        y: {
          ticks: { color: textColor },
          grid: { display: false },
        },
      },
    };
  });

  public timeChartOptions = computed<ChartConfiguration<'bar'>['options']>(() => {
    const isDark = this.themeService.theme() === THEMES.DARK;
    const textColor = isDark ? '#ffffff' : '#0f172a';
    const gridColor = isDark ? 'rgba(255, 255, 255, 0.08)' : 'rgba(15, 23, 42, 0.08)';
    const surfaceColor = isDark ? '#1e293b' : '#ffffff';

    return {
      indexAxis: 'y',
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        legend: { display: false },
        tooltip: {
          backgroundColor: surfaceColor,
          titleColor: textColor,
          bodyColor: textColor,
          borderColor: gridColor,
          borderWidth: 1,
          callbacks: {
            label: (context) => this.formatTime(context.parsed.x ?? 0),
          },
        },
      },
      scales: {
        x: {
          beginAtZero: true,
          ticks: { color: textColor, callback: (value) => this.formatTime(Number(value)) },
          grid: { color: gridColor },
        },
        y: {
          ticks: { color: textColor },
          grid: { display: false },
        },
      },
    };
  });

  public loadLeaderboard(): void {
    this.isLoading.set(true);
    this.hasError.set(false);

    this.leaderboardService.getLeaderboard().pipe(
      finalize(() => this.isLoading.set(false))
    ).subscribe({
      next: (response) => {
        if (response.success) {
          this.leaderboard.set(response.data ?? null);
        } else {
          this.hasError.set(true);
        }
      },
      error: () => {
        this.hasError.set(true);
      }
    });
  }
}

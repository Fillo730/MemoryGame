//Angular
import { Component, inject, signal } from '@angular/core';

//Components
import { GenericCardComponent } from '../generic-card/generic-card.component';
import { MemoryGameComponent } from '../memory-game/memory-game.component';
import { StateHandlerComponent } from '../state-handler/state-handler.component';

//Models
import { Difficulty } from '../../models/entitiesDto/Difficulty.model';

//Services
import { DifficultiesService } from '../../services/difficulties-service.service';
import { AuthService } from '../../services/auth-service.service';

//i18n
import { TranslateModule } from '@ngx-translate/core';
import { finalize, pipe } from 'rxjs';

@Component({
  selector: 'game-handler-component',
  imports: [GenericCardComponent, MemoryGameComponent, TranslateModule, StateHandlerComponent],
  templateUrl: './game-handler.component.html',
  styleUrl: './game-handler.component.css',
})
export class GameHandlerComponent {
  private difficultiesService = inject(DifficultiesService);
  private authService = inject(AuthService);

  public difficulties !: Difficulty[];
  public isLoading = signal(false);
  public hasError = signal(false);
  public isDifficultySelected = signal(false);
  public selectedDifficulty = signal<Difficulty | null>(null);

  ngOnInit() {
    this.loadDifficulties();
  }

  public handleGoBack() {
    this.isDifficultySelected.set(false);
    this.selectedDifficulty.set(null);
  }

  public handleRetryError() : void {
    this.loadDifficulties();
  }

  public isLoggedIn() : boolean {
    return this.authService.isLoggedIn();
  }

  public handleClick(difficulty : Difficulty) : void {
    this.isDifficultySelected.set(true);
    this.selectedDifficulty.set(difficulty);
  }

  private loadDifficulties() {
    this.isLoading.set(true);
    this.hasError.set(false);

    this.difficultiesService.getDifficulties().pipe(
      finalize(() => this.isLoading.set(false))
    )
    .subscribe({
      next: (response => {
        if(response.success) {
          this.difficulties = response.data;
        } else {
          this.hasError.set(true);
        }
      }),
      error: (err) => {
        this.hasError.set(true);
      }
    })
  }
}

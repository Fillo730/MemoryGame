//Angular
import { Component, effect, inject, signal } from '@angular/core';

//Components
import { GenericCardComponent } from '../generic-card/generic-card.component';
import { MemoryGameComponent } from '../memory-game/memory-game.component';
import { StateHandlerComponent } from '../state-handler/state-handler.component';

//Models
import { Difficulty } from '../../models/entitiesDto/Difficulty.model';

//Services
import { DifficultiesService } from '../../services/difficulties-service.service';
import { AuthService } from '../../services/auth-service.service';
import { LanguageService } from '../../services/language-service.service';

//rxjs
import { finalize, pipe } from 'rxjs';

//i18n
import { TranslateModule } from '@ngx-translate/core';

//Helpers
import { isLastElement } from '../../helpers/arrayFunctions.helper';

@Component({
  selector: 'game-handler-component',
  imports: [GenericCardComponent, MemoryGameComponent, TranslateModule, StateHandlerComponent],
  templateUrl: './game-handler.component.html',
  styleUrl: './game-handler.component.css',
})
export class GameHandlerComponent {
  private difficultiesService = inject(DifficultiesService);
  private languageService = inject(LanguageService)
  public authService = inject(AuthService);

  public difficulties !: Difficulty[];
  public isLoading = signal(false);
  public hasError = signal(false);
  public isDifficultySelected = signal(false);
  public selectedDifficulty = signal<Difficulty | null>(null);

  constructor() {
    effect(() => {
      this.languageService.language();
      if(this.selectedDifficulty() == null) {
        this.loadDifficulties();
      }
    })
  }

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

  public handleClick(difficulty : Difficulty) : void {
    this.isDifficultySelected.set(true);
    this.selectedDifficulty.set(difficulty);
  }

  public isLastDiffuculty() : boolean {
    return isLastElement(this.difficulties, this.selectedDifficulty());
  }

  public goToNextDifficulty() : void {
    const currentIndex = this.difficulties.findIndex(difficulty => difficulty.id == this.selectedDifficulty()?.id);
    this.selectedDifficulty.set(this.difficulties.at(currentIndex+1)!);
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

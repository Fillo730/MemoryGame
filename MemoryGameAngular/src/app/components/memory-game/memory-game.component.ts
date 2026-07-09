//Angular
import { Component, Input, Output, EventEmitter, OnInit, OnDestroy, signal, effect, inject } from '@angular/core';
import { CommonModule } from '@angular/common';

//Components
import { GenericButtonComponent } from '../generic-button/generic-button.component';

//Models
import { Difficulty } from '../../models/entitiesDto/Difficulty.model';
import { GameResult } from '../../models/entitiesDto/GameResult.model';

//Services
import { AuthService } from '../../services/auth-service.service';
import { TranslatePipe, TranslateService } from '@ngx-translate/core';
import { GameResultsService } from '../../services/gameResults-service.service';

//Helpers
import { shuffleArray, sampleArray } from '../../helpers/arrayFunctions.helper';
import { scrollToBottom } from '../../helpers/scrollFunctions.helper';
import { formatDuration } from '../../helpers/timeFunctions.helper';

//public
import { defaultImages, placeholder } from '../../../../public/DefaultImages';

@Component({
  selector: 'memory-game-component',
  standalone: true,
  imports: [CommonModule, GenericButtonComponent, TranslatePipe],
  templateUrl: './memory-game.component.html',
  styleUrl: './memory-game.component.css'
})

export class MemoryGameComponent implements OnDestroy {
  private authService = inject(AuthService);
  private gameResultsService = inject(GameResultsService);
  private translateService = inject(TranslateService);

  public placeholderImage = placeholder;

  @Input({ required: true }) difficulty!: Difficulty;
  @Input() nextDifficultyDisabled : boolean = false;
  @Output() goBack = new EventEmitter<void>();
  @Output() nextDifficulty = new EventEmitter<Difficulty>();

  public shuffledImages = signal<string[]>([]);
  public selectedIndexes = signal<number[]>([]);
  public matchedIndexes = signal<number[]>([]);
  public moves = signal(0);
  public isCompleted = signal(false);
  public columns = signal(2);
  public elapsedSeconds = signal(0);

  private static readonly MIN_COLUMNS = 2;
  private static readonly MAX_COLUMNS = 10;
  private timerHandle: ReturnType<typeof setInterval> | null = null;

  constructor() {
    effect(() => {
      if (this.isCompleted() && this.authService.isLoggedIn()) {
        this.saveScore();
      }
    });

    effect(() => {
      if(this.isCompleted()) scrollToBottom();
    })
  }

  ngOnChanges() {
    this.columns.set(this.getDefaultColumns());
    this.resetGame();
  }

  ngOnDestroy() {
    this.stopTimer();
  }

  public formatTime(totalSeconds: number): string {
    return formatDuration(totalSeconds);
  }

  public getMaxColumns(): number {
    return Math.min(MemoryGameComponent.MAX_COLUMNS, this.getTotalCards());
  }

  public getRowNumbers(): number[] {
    const rows = Math.ceil(this.getTotalCards() / this.columns());
    return Array.from({ length: rows }, (_, i) => i + 1);
  }

  public getColumnNumbers(): number[] {
    return Array.from({ length: this.columns() }, (_, i) => i + 1);
  }

  public getCardGridRow(index: number): number {
    return Math.floor(index / this.columns()) + 2;
  }

  public getCardGridColumn(index: number): number {
    return (index % this.columns()) + 2;
  }

  public isCardFlipped(index: number): boolean {
    return this.selectedIndexes().includes(index) || this.matchedIndexes().includes(index);
  }

  public isCardMatched(index: number): boolean {
    return this.matchedIndexes().includes(index);
  }

  public getCardAriaLabel(index: number): string {
    const row = Math.floor(index / this.columns()) + 1;
    const col = (index % this.columns()) + 1;
    const stateKey = this.isCardMatched(index)
      ? 'Play.CardStateMatched'
      : this.isCardFlipped(index)
        ? 'Play.CardStateRevealed'
        : 'Play.CardStateCovered';
    const state = this.translateService.instant(stateKey);

    return this.translateService.instant('Play.CardAriaLabel', { row, col, state });
  }

  public increaseColumns(): void {
    this.columns.update(c => Math.min(this.getMaxColumns(), c + 1));
  }

  public decreaseColumns(): void {
    this.columns.update(c => Math.max(MemoryGameComponent.MIN_COLUMNS, c - 1));
  }

  private getTotalCards(): number {
    return Math.floor(this.difficulty.numberOfPairs) * 2;
  }

  private getDefaultColumns(): number {
    const total = this.getTotalCards();
    const squareEstimate = Math.round(Math.sqrt(total));

    return Math.min(this.getMaxColumns(), Math.max(MemoryGameComponent.MIN_COLUMNS, squareEstimate));
  }

  public resetGame() {
    const numPairs = Math.floor(this.difficulty.numberOfPairs);
    const selectedPool =sampleArray<string>(defaultImages, numPairs);
    const newShuffled = shuffleArray<string>([...selectedPool, ...selectedPool]);

    this.shuffledImages.set(newShuffled);
    this.selectedIndexes.set([]);
    this.matchedIndexes.set([]);
    this.moves.set(0);
    this.isCompleted.set(false);
    this.startTimer();
  }

  private startTimer(): void {
    this.stopTimer();
    this.elapsedSeconds.set(0);
    this.timerHandle = setInterval(() => this.elapsedSeconds.update(s => s + 1), 1000);
  }

  private stopTimer(): void {
    if (this.timerHandle !== null) {
      clearInterval(this.timerHandle);
      this.timerHandle = null;
    }
  }

  public handleCardClick(index: number) {
    const currentSelected = this.selectedIndexes();
    const currentMatched = this.matchedIndexes();

    if (currentSelected.length === 2 || currentMatched.includes(index) || currentSelected.includes(index)) {
      return;
    }

    const newSelected = [...currentSelected, index];
    this.selectedIndexes.set(newSelected);

    if (newSelected.length === 2) {
      this.moves.update(m => m + 1);
      this.checkMatch(newSelected);
    }
  }

  private checkMatch(selected: number[]) {
    const images = this.shuffledImages();
    
    if (images[selected[0]] === images[selected[1]]) {
      this.matchedIndexes.update(prev => [...prev, ...selected]);
      this.selectedIndexes.set([]);
      
      if (this.matchedIndexes().length === images.length) {
        this.stopTimer();
        this.isCompleted.set(true);
      }
    } else {
      setTimeout(() => {
        this.selectedIndexes.set([]);
      }, 1000);
    }
  }

  private saveScore() {
    const gameResult: GameResult = {
        moves: this.moves(),
        durationSeconds: this.elapsedSeconds(),
        difficulty: this.difficulty,
        playedAt: new Date()
    };

    this.gameResultsService.saveGameResults(gameResult).subscribe({
        next: (response) => {
            if (response.success) {
                
            } else {
                console.error('Errore nel salvataggio:', response.message);
            }
        },
        error: (err) => console.error('Errore nel salvataggio:', err)
    });
}
}
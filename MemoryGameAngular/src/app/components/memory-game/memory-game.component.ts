//Angular
import { Component, Input, Output, EventEmitter, OnInit, signal, effect, inject } from '@angular/core';
import { CommonModule } from '@angular/common';

//Components
import { GenericButtonComponent } from '../generic-button/generic-button.component';

//Models
import { Difficulty } from '../../models/entitiesDto/Difficulty.model';

//Services
import { AuthService } from '../../services/auth-service.service';
import { TranslatePipe } from '@ngx-translate/core';
import { GameResultsService } from '../../services/gameResults-service.service';

//Helpers
import { shuffleArray, sampleArray } from '../../helpers/arrayFunctions.helper';

//public
import { defaultImages, placeholder } from '../../../../public/DefaultImages';
import { GameResult } from '../../models/entitiesDto/GameResult.model';

@Component({
  selector: 'memory-game-component',
  standalone: true,
  imports: [CommonModule, GenericButtonComponent, TranslatePipe],
  templateUrl: './memory-game.component.html',
  styleUrl: './memory-game.component.css'
})

export class MemoryGameComponent implements OnInit {
  private authService = inject(AuthService);
  private gameResultsService = inject(GameResultsService)

  @Input({ required: true }) difficulty!: Difficulty;
  @Output() goBack = new EventEmitter<void>();
  @Output() nextDifficulty = new EventEmitter<Difficulty>();

  public shuffledImages = signal<string[]>([]);
  public displayArray = signal<string[]>([]);
  public selectedIndexes = signal<number[]>([]);
  public matchedIndexes = signal<number[]>([]);
  public moves = signal(0);
  public isCompleted = signal(false);

  constructor() {
    effect(() => {
      if (this.isCompleted() && this.authService.isLoggedIn()) {
        this.saveScore();
      }
    });
  }

  ngOnInit() {
    this.resetGame();
  }

  public resetGame() {
    const numPairs = Math.floor(this.difficulty.numberOfPairs);
    const selectedPool =sampleArray<string>(defaultImages, numPairs);
    const newShuffled = shuffleArray<string>([...selectedPool, ...selectedPool]);

    this.shuffledImages.set(newShuffled);
    this.displayArray.set(new Array(newShuffled.length).fill(placeholder));
    this.selectedIndexes.set([]);
    this.matchedIndexes.set([]);
    this.moves.set(0);
    this.isCompleted.set(false);
  }

  public handleCardClick(index: number) {
    const currentSelected = this.selectedIndexes();
    const currentMatched = this.matchedIndexes();

    if (currentSelected.length === 2 || currentMatched.includes(index) || currentSelected.includes(index)) {
      return;
    }

    const newDisplay = [...this.displayArray()];
    newDisplay[index] = this.shuffledImages()[index];
    this.displayArray.set(newDisplay);

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
        this.isCompleted.set(true);
      }
    } else {
      setTimeout(() => {
        const resetDisplay = [...this.displayArray()];
        resetDisplay[selected[0]] = placeholder;
        resetDisplay[selected[1]] = placeholder;
        this.displayArray.set(resetDisplay);
        this.selectedIndexes.set([]);
      }, 1000);
    }
  }

  private saveScore() {
    const gameResult: GameResult = {
        moves: this.moves(),
        difficulty: this.difficulty,
        playedAt: new Date()
    };

    this.gameResultsService.saveGameResults(gameResult).subscribe({
        next: (response) => {
            console.log('Risultato salvato con ID:', response.data.id);
        },
        error: (err) => console.error('Errore nel salvataggio:', err)
    });
}
}
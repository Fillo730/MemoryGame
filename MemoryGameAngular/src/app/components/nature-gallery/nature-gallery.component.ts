//angular
import { Component, Input, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';

//components
import { CustomButtonComponent } from '../custom-button/custom-button.component';

//helper
import { shuffleArray } from '../../helpers/arrayFunctions.helper';

@Component({
  selector: 'nature-gallery-component',
  standalone: true,
  imports: [CommonModule, CustomButtonComponent],
  templateUrl: './nature-gallery.component.html',
  styleUrl: './nature-gallery.component.css',
})
export class NatureGalleryComponent implements OnInit {
  @Input() text !: string;
  @Input() buttonLabel !: string;
  @Input() defaultImages : string[] = [];
  @Input() numberOfImagesShowed : number = 8;

  private images !: string[];
  public randomImages: string[] = [];

  ngOnInit(): void {
    this.images = this.defaultImages;
    this.randomImages = shuffleArray([...this.images]);
  }

  public handleClick(): void {
    this.randomImages = shuffleArray(this.images);
  }
}
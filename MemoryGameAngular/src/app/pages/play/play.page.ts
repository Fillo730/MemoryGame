//Angular
import { Component } from '@angular/core';

//Components
import { HeaderComponent } from '../../components/header/header.component';
import { FooterComponent } from '../../components/footer/footer.component';
import { GameHandlerComponent } from '../../components/game-handler/game-handler.component';

@Component({
  selector: 'play-page',
  imports: [GameHandlerComponent,HeaderComponent,FooterComponent],
  templateUrl: './play.page.html',
  styleUrl: './play.page.css',
})

export class PlayPage {

}

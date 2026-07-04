import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { ThemeService } from './services/theme-service.service';
import { LanguageService } from './services/language-service.service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet],
  templateUrl: './app.html',
})

export class App {
  // Injected here (unused directly) so their theme/language init side effects
  // run on bootstrap regardless of route, instead of only when a page happens
  // to render header-component (e.g. login/signup don't).
  private themeService = inject(ThemeService);
  private languageService = inject(LanguageService);
}

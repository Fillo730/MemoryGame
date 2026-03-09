//Angular
import { Component, inject } from '@angular/core';

//Services
import { LanguageService } from '../../services/language-service.service';

//Constants
import { APP_CONFIG } from '../../constants/app.config';

//Models
import { LanguageType } from '../../models/types/Language.model';

@Component({
  selector: 'language-picker-component',
  imports: [],
  templateUrl: './language-picker.component.html',
  styleUrl: './language-picker.component.css',
})

export class LanguagePickerComponent {
  public languageService = inject(LanguageService);

  public appConfig = APP_CONFIG;

  public changeLanguage(lang: string) {
    this.languageService.setLanguage(lang as LanguageType);
  }
}

//Angular
import { Component, computed, inject } from '@angular/core';

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

  public currentLangOption = computed(() =>
    this.appConfig.LANG_OPTIONS.find(lang => lang.value === this.languageService.language())
  );

  public changeLanguage(lang: string) {
    this.languageService.setLanguage(lang as LanguageType);
  }
}

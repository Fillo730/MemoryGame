//Angular
import { Component, inject } from '@angular/core';

//Services
import { LanguageService } from '../../services/language-service.service';

//Models
import { Language } from '../../models/Language.model';

@Component({
  selector: 'language-picker-component',
  imports: [],
  templateUrl: './language-picker.component.html',
  styleUrl: './language-picker.component.css',
})

export class LanguagePickerComponent {
  public languageService = inject(LanguageService);

  public changeLanguage(lang: string) {
    this.languageService.setLanguage(lang as Language);
  }
}

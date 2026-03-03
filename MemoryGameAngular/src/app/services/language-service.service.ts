//Angular
import { effect, inject, Injectable, signal } from '@angular/core';

//Model
import { Language } from '../models/Language.model';

//Utils
import { isLocalStorageValid } from '../utils/window-guart.util';

//i18n
import { TranslateService } from '@ngx-translate/core';
import { DEFAULT_LANGUAGE, STORAGE_KEYS } from '../utils/storage_keys';

@Injectable({
  providedIn: 'root',
})

export class LanguageService {
    
    private readonly storageKey : string = STORAGE_KEYS.USER_LANGUAGE;
    private readonly defaultLanguage : Language = DEFAULT_LANGUAGE as Language;
    
    private translate = inject(TranslateService);

    public language = signal<Language>(this.getStoredLanguage());

    public readonly supportedLanguages = [
        { code: 'it' as Language, label: 'Italiano', icon: 'language_p_it' },
        { code: 'en' as Language, label: 'English', icon: 'public' } 
    ];

    constructor() {
        this.translate.setFallbackLang(this.defaultLanguage);
        this.translate.use(this.language());

        effect(() => {
            const currentLanguage = this.language();
            
            this.translate.use(currentLanguage);
            
            if(isLocalStorageValid()) {
                localStorage.setItem(this.storageKey, currentLanguage);
            }
        })
    }

    public setLanguage(newLanguage : Language) : void {
        this.language.set(newLanguage);
    }

    private getStoredLanguage() : Language {
        if(isLocalStorageValid()) {
            return (localStorage.getItem(this.storageKey) as Language) ?? this.defaultLanguage;
        }
        return this.defaultLanguage;
    }
}

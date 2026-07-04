//Angular
import { effect, inject, Injectable, signal } from '@angular/core';

//Model
import { LanguageType } from '../models/types/Language.model';

//Utils
import { isLocalStorageValid } from '../utils/window-guart.util';

//i18n
import { TranslateService } from '@ngx-translate/core';

//Constants
import { STORAGE_KEYS } from '../constants/storage_keys';
import { APP_CONFIG } from '../constants/app.config';

@Injectable({
  providedIn: 'root',
})

export class LanguageService {
    
    private readonly storageKey : string = STORAGE_KEYS.USER_LANGUAGE;
    private readonly defaultLanguage : LanguageType = APP_CONFIG.DEFAULT_LANGUAGE as LanguageType;
    
    private translate = inject(TranslateService);

    public language = signal<LanguageType>(this.getStoredLanguage());

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

    public setLanguage(newLanguage : LanguageType) : void {
        this.language.set(newLanguage);
    }

    private getStoredLanguage() : LanguageType {
        if(isLocalStorageValid()) {
            return (localStorage.getItem(this.storageKey) as LanguageType) ?? this.defaultLanguage;
        }
        return this.defaultLanguage;
    }
}

//Angular
import { DOCUMENT, effect, inject, Injectable, signal } from '@angular/core';

//Constants
import { APP_CONFIG } from '../constants/app.config';

//Models
import { THEMES, ThemeType } from '../models/types/Theme.model';

//Utils
import { isLocalStorageValid } from '../utils/window-guart.util';
import { STORAGE_KEYS } from '../constants/storage_keys';

@Injectable({
  providedIn: 'root',
})

export class ThemeService {
    private readonly storageKey : string = STORAGE_KEYS.USER_THEME;
    private readonly defaultTheme : ThemeType = APP_CONFIG.DEFAULT_THEME;

    private readonly document = inject(DOCUMENT);

    public theme = signal<ThemeType>(this.getStoredTheme());

    constructor() {
        effect(() => {
            const currentTheme = this.theme();
            
            if(isLocalStorageValid()) {
                localStorage.setItem(this.storageKey, currentTheme);

                if(currentTheme === THEMES.DARK) {
                    this.document.body.classList.add("dark-theme");
                    this.document.body.classList.remove("light-theme");
                } else {
                    this.document.body.classList.add("light-theme");
                    this.document.body.classList.remove("dark-theme");
                }
            }
        })
    }

    public setTheme(newTheme : ThemeType) : void {
        this.theme.set(newTheme);
    }

    public toggleTheme() : void {
        this.theme.update(currentTheme => currentTheme === THEMES.DARK ? THEMES.LIGHT : THEMES.DARK);
    }

    private getStoredTheme() : ThemeType {
        if(isLocalStorageValid()) {
             return (localStorage.getItem(this.storageKey) as ThemeType) ?? this.defaultTheme; 
        }
        return this.defaultTheme;
    }
}

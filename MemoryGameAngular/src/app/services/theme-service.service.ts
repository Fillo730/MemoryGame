//Angular
import { DOCUMENT, effect, inject, Injectable, signal } from '@angular/core';

//Models
import { Theme } from '../models/Theme.model';

//Utils
import { isLocalStorageValid } from '../utils/window-guart.util';
import { DEFAULT_THEME, STORAGE_KEYS } from '../utils/storage_keys';

@Injectable({
  providedIn: 'root',
})

export class ThemeService {
    private readonly storageKey : string = STORAGE_KEYS.USER_THEME;
    private readonly defaultTheme : Theme = DEFAULT_THEME;

    private readonly document = inject(DOCUMENT);

    public theme = signal<Theme>(this.getStoredTheme());

    constructor() {
        effect(() => {
            const currentTheme = this.theme();
            
            if(isLocalStorageValid()) {
                localStorage.setItem(this.storageKey, currentTheme);

                if(currentTheme === "dark") {
                    this.document.body.classList.add("dark-theme");
                    this.document.body.classList.remove("light-theme");
                } else {
                    this.document.body.classList.add("light-theme");
                    this.document.body.classList.remove("dark-theme");
                }
            }
        })
    }

    public setTheme(newTheme : Theme) : void {
        this.theme.set(newTheme);
    }

    public toggleTheme() : void {
        this.theme.update(currentTheme => currentTheme === "dark" ? "light" : "dark");
    }

    private getStoredTheme() : Theme {
        if(isLocalStorageValid()) {
             return (localStorage.getItem(this.storageKey) as Theme) ?? this.defaultTheme; 
        }
        return this.defaultTheme;
    }
}

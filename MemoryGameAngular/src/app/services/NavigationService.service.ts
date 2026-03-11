//Angular
import { inject, Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { Location } from "@angular/common";

//Services
import { LanguageService } from "./language-service.service";
import { TranslateService } from "@ngx-translate/core";

//Utils
import { isWindowValid } from "../utils/window-guart.util";
import { isWindowHistoryValid } from "../utils/window-guart.util";

export enum AppRoutes {
    HOME = 'home',
    LOGIN = 'login',
    PROFILE = 'profile',
    PLAY = 'play',
    SIGNUP = 'signup',
    ABOUT = 'about'
}

@Injectable({
    providedIn: 'root'
})
export class NavigationService {
    private router = inject(Router);
    private languageService = inject(LanguageService);
    private translateService = inject(TranslateService);
    private location = inject(Location);

    public getLocalizedRoute(routeKey: AppRoutes): string {
        const lang = this.languageService.language();
        const translatedRoute = this.translateService.instant(`Routes.${routeKey}`);
        return `/${lang}/${translatedRoute}`;
    }

    public goToGenericUrl(url : string) : void {
        if(isWindowValid() && url) {
            window.open(url, '_blank', 'noopener noreferrer');
        }
    }

    public goToHome(): void {
        this.navigateTo(AppRoutes.HOME);
    }

    public goToLogin(): void {
        this.navigateTo(AppRoutes.LOGIN);
    }

    public goToProfile(): void {
        this.navigateTo(AppRoutes.PROFILE);
    }

    public goToPlay(): void {
        this.navigateTo(AppRoutes.PLAY);
    }

    public goToSignUp(): void {
        this.navigateTo(AppRoutes.SIGNUP);
    }

    public goBack() : void {
        if(isWindowHistoryValid() && window.history.length > 1) {
            this.location.back();
        } else {
            this.goToHome();
        }
    }

    private navigateTo(routeKey: AppRoutes): void {
        this.router.navigate([this.getLocalizedRoute(routeKey)]);
    }
}
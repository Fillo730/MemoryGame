import { ApplicationConfig, provideBrowserGlobalErrorListeners, provideZonelessChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideTranslateHttpLoader, TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient, provideHttpClient, withInterceptors } from '@angular/common/http';
import { provideTranslateService } from '@ngx-translate/core';
import { languageInterceptor } from './interceptors/languageInterceptor.interceptor';
import { jwtInterceptor } from './interceptors/jwtInterceptor.intercept';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZonelessChangeDetection(),
    provideRouter(routes),
    provideHttpClient(
      withInterceptors([languageInterceptor, jwtInterceptor])
    ),
    provideTranslateService({
      loader: provideTranslateHttpLoader({ prefix: './i18n/', suffix: '.json' }),
      fallbackLang: 'it'
    }),
  ]
};

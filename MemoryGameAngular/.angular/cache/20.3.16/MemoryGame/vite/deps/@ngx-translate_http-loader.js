import {
  HttpBackend,
  HttpClient
} from "./chunk-RNHH65DU.js";
import "./chunk-R6U7IGMG.js";
import {
  TranslateLoader
} from "./chunk-FIFJNUS6.js";
import {
  Injectable,
  InjectionToken,
  inject,
  setClassMetadata,
  ɵɵdefineInjectable
} from "./chunk-BVED4GYE.js";
import {
  __spreadValues
} from "./chunk-PJVWDKLX.js";

// node_modules/@ngx-translate/http-loader/fesm2022/ngx-translate-http-loader.mjs
var TRANSLATE_HTTP_LOADER_CONFIG = new InjectionToken("TRANSLATE_HTTP_LOADER_CONFIG");
var TranslateHttpLoader = class _TranslateHttpLoader {
  http;
  config;
  constructor() {
    this.config = __spreadValues({
      prefix: "/assets/i18n/",
      suffix: ".json",
      enforceLoading: false,
      useHttpBackend: false
    }, inject(TRANSLATE_HTTP_LOADER_CONFIG));
    this.http = this.config.useHttpBackend ? new HttpClient(inject(HttpBackend)) : inject(HttpClient);
  }
  /**
   * Gets the translations from the server
   */
  getTranslation(lang) {
    const cacheBuster = this.config.enforceLoading ? `?enforceLoading=${Date.now()}` : "";
    return this.http.get(`${this.config.prefix}${lang}${this.config.suffix}${cacheBuster}`);
  }
  static ɵfac = function TranslateHttpLoader_Factory(__ngFactoryType__) {
    return new (__ngFactoryType__ || _TranslateHttpLoader)();
  };
  static ɵprov = ɵɵdefineInjectable({
    token: _TranslateHttpLoader,
    factory: _TranslateHttpLoader.ɵfac
  });
};
(() => {
  (typeof ngDevMode === "undefined" || ngDevMode) && setClassMetadata(TranslateHttpLoader, [{
    type: Injectable
  }], () => [], null);
})();
function provideTranslateHttpLoader(config = {}) {
  const useBackend = config.useHttpBackend ?? false;
  return [{
    provide: TRANSLATE_HTTP_LOADER_CONFIG,
    useValue: config
  }, {
    provide: TranslateLoader,
    useClass: TranslateHttpLoader,
    deps: [useBackend ? HttpBackend : HttpClient, TRANSLATE_HTTP_LOADER_CONFIG]
  }];
}
export {
  TRANSLATE_HTTP_LOADER_CONFIG,
  TranslateHttpLoader,
  provideTranslateHttpLoader
};
//# sourceMappingURL=@ngx-translate_http-loader.js.map

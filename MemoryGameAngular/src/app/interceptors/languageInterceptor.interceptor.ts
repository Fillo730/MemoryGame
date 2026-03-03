//Angular
import { HttpInterceptorFn } from "@angular/common/http";

//Utils
import { isLocalStorageValid } from "../utils/window-guart.util";
import { DEFAULT_LANGUAGE, STORAGE_KEYS } from "../utils/storage_keys";

//Models
import { Language } from "@ngx-translate/core";

export const languageInterceptor : HttpInterceptorFn = (req, next) => {
    const storagekey : string = STORAGE_KEYS.USER_LANGUAGE;
    const defaultLanguage : Language = DEFAULT_LANGUAGE;

    let language !: Language; 
    
    if(isLocalStorageValid()) {
        language = (localStorage.getItem(storagekey) ?? defaultLanguage) as Language;
    } else {
        language = defaultLanguage;
    }
    
    const authReq = req.clone({
        setParams: { lang: language }
    })

    return next(authReq);
}
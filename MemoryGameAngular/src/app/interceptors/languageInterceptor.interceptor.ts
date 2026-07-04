//Angular
import { HttpInterceptorFn } from "@angular/common/http";

//Constants
import { APP_CONFIG } from "../constants/app.config";

//Utils
import { isLocalStorageValid } from "../utils/window-guart.util";
import { STORAGE_KEYS } from "../constants/storage_keys";

//Models
import { LanguageType } from "../models/types/Language.model";

export const languageInterceptor : HttpInterceptorFn = (req, next) => {
    const storagekey : string = STORAGE_KEYS.USER_LANGUAGE;
    const defaultLanguage : LanguageType = APP_CONFIG.DEFAULT_LANGUAGE;

    let language !: LanguageType; 
    
    if(isLocalStorageValid()) {
        language = (localStorage.getItem(storagekey) ?? defaultLanguage) as LanguageType;
    } else {
        language = defaultLanguage;
    }
    
    const authReq = req.clone({
        setParams: { lang: language }
    })

    return next(authReq);
}
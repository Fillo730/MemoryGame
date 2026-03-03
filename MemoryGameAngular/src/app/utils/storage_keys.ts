//Models
import { Language } from "../models/Language.model";
import { Theme } from "../models/Theme.model";

export const STORAGE_KEYS = {
    USER_LANGUAGE: "user-language",
    USER_THEME: "user-theme",
    USER_INFO: "user-info"
}

export const DEFAULT_LANGUAGE : Language = "it";

export const DEFAULT_THEME : Theme = "dark";
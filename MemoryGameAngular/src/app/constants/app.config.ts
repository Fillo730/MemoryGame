//Models
import { LANGUAGES } from "../models/types/Language.model";
import { THEMES } from "../models/types/Theme.model";

export const APP_CONFIG = {
    DEFAULT_LANGUAGE: LANGUAGES.ITALIANO,
    SUPPORTED_LANGUAGES: [LANGUAGES.ITALIANO, LANGUAGES.ENGLISH],
    LANG_OPTIONS: [
        { label: "Italiano", value: LANGUAGES.ITALIANO },
        { label: "English", value: LANGUAGES.ENGLISH },
    ],
    DEFAULT_THEME: THEMES.DARK,
} as const;

export const API_BASE_URL = 'https://localhost:7015/api';

export const API_ENDPOINTS =  {
    AUTH: "auth",
    DIFFICULTIES: "difficulties",
    GAME_RESULTS: "gameResults",
    USERS: "users"
} as const;

export function getApiUrl (key : keyof typeof API_ENDPOINTS) : string {
    return `${API_BASE_URL}/${API_ENDPOINTS[key]}`;
}
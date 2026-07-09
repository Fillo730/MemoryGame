import { environment } from "../../environments/environment";

//Models
import { LANGUAGES } from "../models/types/Language.model";
import { THEMES } from "../models/types/Theme.model";

export const APP_CONFIG = {
    DEFAULT_LANGUAGE: LANGUAGES.ITALIANO,
    SUPPORTED_LANGUAGES: [LANGUAGES.ITALIANO, LANGUAGES.ENGLISH, LANGUAGES.FRANCESE, LANGUAGES.TEDESCO],
    LANG_OPTIONS: [
        { label: "Italiano", value: LANGUAGES.ITALIANO, flag: "🇮🇹" },
        { label: "English", value: LANGUAGES.ENGLISH, flag: "🇬🇧" },
        { label: "Français", value: LANGUAGES.FRANCESE, flag: "🇫🇷" },
        { label: "Deutsch", value: LANGUAGES.TEDESCO, flag: "🇩🇪" },
    ],
    DEFAULT_THEME: THEMES.DARK,
} as const;

export const API_BASE_URL = environment.apiBaseUrl;

export const API_ENDPOINTS =  {
    AUTH: "auth",
    DIFFICULTIES: "difficulties",
    GAME_RESULTS: "gameResults",
    USERS: "users",
    LEADERBOARD: "leaderboard",
    ACHIEVEMENTS: "achievements",
    FRIENDS: "friends"
} as const;

export function getApiUrl (key : keyof typeof API_ENDPOINTS) : string {
    return `${API_BASE_URL}/${API_ENDPOINTS[key]}`;
}
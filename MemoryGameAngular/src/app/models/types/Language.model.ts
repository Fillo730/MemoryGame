export const LANGUAGES  = {
    ITALIANO : "it",
    ENGLISH : "en",
    FRANCESE : "fr",
    TEDESCO : "de"
} as const;

export type LanguageType = typeof LANGUAGES [keyof typeof LANGUAGES];
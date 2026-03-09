export const LANGUAGES  = {
    ITALIANO : "it",
    ENGLISH : "en"
} as const;

export type LanguageType = typeof LANGUAGES [keyof typeof LANGUAGES];
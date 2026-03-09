export const THEMES = {
    DARK: "dark-mode",
    LIGHT: "light-mode"
} as const;

export type ThemeType = typeof THEMES[keyof typeof THEMES]; 
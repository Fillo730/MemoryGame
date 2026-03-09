export const STORAGE_KEYS = {
    USER_LANGUAGE: "user-language",
    USER_THEME: "user-theme",
    USER_INFO: "user-info"
}

export type StorageKeyValue = typeof STORAGE_KEYS[keyof typeof STORAGE_KEYS];
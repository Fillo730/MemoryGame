export function isLocalStorageValid(): boolean {
    return typeof window !== "undefined" && window.localStorage ? true : false;
}

export function isWindowHistoryValid() : boolean {
    return typeof window !== 'undefined' && window.history ? true : false
}

export function isWindowValid() : boolean {
    return typeof window !== 'undefined'
}
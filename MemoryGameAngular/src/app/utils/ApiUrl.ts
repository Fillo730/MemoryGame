const API_URL = 'https://localhost:7015/api';

export function getApiUrl(controllerKey: string): string {
    return `${API_URL}/${controllerKey}`;
}
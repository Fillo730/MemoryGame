export interface Achievement {
    id: number;
    code: string;
    icon: string;
    name: string;
    description: string;
    unlocked: boolean;
    unlockedAt: string | null;
}

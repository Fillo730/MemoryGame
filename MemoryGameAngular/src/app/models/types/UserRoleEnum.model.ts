export const USER_ROLES = {
    User: 0,
    Admin: 1
} as const;

export type UserRoleType = typeof USER_ROLES[keyof typeof USER_ROLES];
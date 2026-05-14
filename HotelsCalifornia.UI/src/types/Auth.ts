export interface LoginRequest
{
    username: string;
    password: string;
}

export interface LoginResponse
{
    token: string;
    expiresAt: string;
}

export interface AuthUser
{
    userId: number;
    username: string;
    role: 'Member' | 'Manager' | 'Admin';
}


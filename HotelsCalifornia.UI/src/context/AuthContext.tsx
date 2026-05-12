import { createContext, useContext, useState, useMemo, type ReactNode } from "react";
import type { LoginRequest, AuthUser } from "../types/Auth";
import { authService } from "../services/authService";
import { jwtDecode } from "jwt-decode";

interface AuthContextValue
{
    token: string | null;
    user: AuthUser | null;
    isAuthenticated: boolean;
    login: (creds: LoginRequest) => Promise<void>;
    logout: () => void;
}

const AuthContext = createContext<AuthContextValue | undefined>(undefined);

//TODO figure out what this does and change it
const NAMEID_URI = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier';
const NAME_URI = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name';
const ROLE_URI = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';

interface JwtPayLoad
{
    nameid?: string;
    unique_name?: string;
    role?: 'Member' | 'Manager' | 'Admin';
    [NAMEID_URI]?: string;
    [NAME_URI]?: string;
    [ROLE_URI]?: 'Member' | 'Manager' | 'Admin';
    exp: number;
}

function decodeUser(token: string): AuthUser
{
    const payload = jwtDecode<JwtPayLoad>(token);
    const id = payload.nameid ?? payload[NAMEID_URI] ?? '0';
    const name = payload.unique_name ?? payload[NAME_URI] ?? '';
    const role = (payload.role ?? payload[ROLE_URI] ?? 'Member') as 'Member' | 'Manager' | 'Admin';

    return {
        userId: Number(id),
        username: name,
        role
    }
}

export function AuthProvider({ children }: { children: ReactNode })
{
    const [token, setToken] = useState<string | null>(() =>
        localStorage.getItem('token')
    );
    const [user, setUser] = useState<AuthUser | null>(() =>
    {
        const t = localStorage.getItem('token');
        return t ? decodeUser(t) : null;
    });

    const login = async (creds: LoginRequest) =>
    {
        const response = await authService.Login(creds);
        localStorage.setItem('token', response.token);

        setToken(response.token);
        setUser(decodeUser(response.token));
    }

    const logout = () =>
    {
        localStorage.removeItem('token');
        setToken(null);
        setUser(null);

    }

    const value = useMemo<AuthContextValue>(
        () => ({
            token,
            user,
            isAuthenticated: token !== null,
            login,
            logout
        }),
        [token, user]
    );

    return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>
}

export function useAuth(): AuthContextValue
{
    const ctx = useContext(AuthContext);

    if (ctx === undefined)
    {
        throw new Error('useAuth hook MUST be used inside of an <AuthProvider>')
    }
    return ctx;
}
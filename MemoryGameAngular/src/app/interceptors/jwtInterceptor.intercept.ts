//Angular
import { HttpInterceptorFn } from "@angular/common/http";
import { inject } from "@angular/core";

//Services
import { AuthService } from "../services/auth-service.service";

export const jwtInterceptor : HttpInterceptorFn = (req, next) => {
    const authService = inject(AuthService);
    
    const token = authService.currentUser()?.token;

    if(token) {
        req = req.clone({
            setHeaders: {
                Authorization: `Bearer ${token}`
            }
        })
    }

    return next(req);
}
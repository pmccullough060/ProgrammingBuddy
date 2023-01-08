import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { AuthService } from "../services/auth.service";

@Injectable()
export class AuthenticatedUser implements CanActivate {
    constructor(private authService: AuthService, private router: Router){}

    // Checks for the presence of a valid JWT token:
    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {

        if(this.authService.authenticatedUser()){
            return true;
        }

        else{
            this.router.navigate(['/login']);

            return false;
        }
    }
}
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from './services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  public userAuthenticated = false;

  constructor(public _authService: AuthService, public router: Router) {}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      this._authService.loginChanged
      .subscribe(userAuthenticated => {
        this.userAuthenticated = userAuthenticated;
      });

      if (!this.userAuthenticated) {
        this._authService.login();
      }
      return true;
  }
  
}

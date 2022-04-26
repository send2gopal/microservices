import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { from, Observable } from 'rxjs';
import { Guid } from 'guid-typescript';
import { AuthService } from './auth.service';
import {environment} from '../../../environments/environment'

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptorService implements HttpInterceptor {

  constructor(private _authService: AuthService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if(req.url.startsWith(environment.apiRoot)){
      return from(
        this._authService.getAccessToken()
        .then(token => {
          var headers = req.headers.set('Authorization', `Bearer ${token}`)
          .set('x-correlation-id', `${Guid.create()}`);
          const authRequest = req.clone({ headers });
          return next.handle(authRequest).toPromise();
        })
      );
    }
    else {
      return next.handle(req);
    }
  }
}
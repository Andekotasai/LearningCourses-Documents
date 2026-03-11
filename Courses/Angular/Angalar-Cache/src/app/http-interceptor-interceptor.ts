import { HttpErrorResponse, HttpInterceptorFn, HttpResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { catchError, tap, throwError } from 'rxjs';

export const httpInterceptorInterceptor: HttpInterceptorFn = (req, next) => {
  const cookieService = inject(CookieService);
  const router = inject(Router);
  
  // Get auth token from cookie/storage
  const token = cookieService.get('AuthToken');
  
  // Add auth header only if token exists and URL is not login
  if (token && !req.url.includes('/login')) {
    req = req.clone({
      setHeaders: { Authorization: `Bearer ${token}` }
    });
  }
  
  return next(req).pipe(
    tap(event => {
      // Log successful responses in dev mode
      if (event instanceof HttpResponse) {
        console.log('Response received:', event.statusText);
        // Store only status, not entire response
        sessionStorage.setItem('LastResponse', JSON.stringify({ 
          status: event.status, 
          statusText: event.statusText 
        }));
      }
    }),
    catchError((error: HttpErrorResponse) => {
      console.error('HTTP Error:', error.status, error.message);
      
      // Handle 401 - redirect to login
      if (error.status === 401) {
        router.navigate(['/login']);
      }
      
      // Propagate error for component handling
      return throwError(() => error);
    })
  );
};

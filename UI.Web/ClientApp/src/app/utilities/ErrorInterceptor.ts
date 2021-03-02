import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { GodService } from '../services/god.service';
/**
 * Intercepts the HTTP responses, and in case that an error/exception is thrown, handles it
 * and extract the relevant information of it.
 */
@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    /**
     * Intercepts an outgoing HTTP request, executes it and handles any error that could be triggered in execution.
     * @see HttpInterceptor
     * @param req the outgoing HTTP request
     * @param next a HTTP request handler
     */
    /**
     *
     */
    constructor(private god: GodService) {
        
    }
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe()
            .pipe(catchError(errorResponse => {                
                let errMsg: string = '';
                if (errorResponse instanceof HttpErrorResponse) {
                    if (errorResponse.status === 400 && errorResponse.error && errorResponse.error.errors.length) {
                        this.god.notifications.dangerList(errorResponse.error.errors);
                        return;
                    }
                    else{
                        const err = errorResponse.message || JSON.stringify(errorResponse.error.errors);
                        errMsg = `${errorResponse.status} - ${errorResponse.statusText || ''} Details: ${err}`;
                    }
                } else {
                    errMsg = errorResponse.message ? errorResponse.message : errorResponse.toString();
                }
                return throwError(errMsg);
            }));
    }
}

/**
 * Provider POJO for the interceptor
 */
export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true,
};
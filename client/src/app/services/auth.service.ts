import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ICompileRequestModel } from '../models/compileRequestModel';
import { ICompilationResponseModel } from '../models/compilationResponseModel';
import { ILoginRequestModel } from '../models/LoginRequestModel';
import { ILoginResponseModel } from '../models/loginResponseModel';
import { IRegisterRequestModel } from '../models/RegisterRequestModel';

@Injectable({
    providedIn: 'root',
})
export class AuthService {
    constructor(private http: HttpClient) {}

    login(model: ILoginRequestModel): Observable<ILoginResponseModel> {
        return this.http.post<ILoginResponseModel>(`http://localhost:7071/api/Login`, model);
    }

    setSession(loginResponse: ILoginResponseModel){
        localStorage.setItem('token', loginResponse.token);
        localStorage.setItem('expiry', JSON.stringify(loginResponse.expiry.valueOf()));
    }

    logout() {
        localStorage.removeItem("token");
        localStorage.removeItem("expiry");
    }

    register(model: IRegisterRequestModel){
        return this.http.post<ILoginResponseModel>(`http://localhost:7071/api/Register`, model);
    }

    // TODO: use dayjs to check date before etc. etc. for logged out status
}

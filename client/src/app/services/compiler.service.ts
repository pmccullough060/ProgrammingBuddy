import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ICompileRequestModel } from '../models/compileRequestModel';

@Injectable({
    providedIn: 'root',
})
export class CompilerService {
    constructor(private http: HttpClient) {}

    compileCode(model: ICompileRequestModel): Observable<string>{
        console.log(model);
        return this.http.post<string>(`http://localhost:7071/api/Compile`, model)
    }
}


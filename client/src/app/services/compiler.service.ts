import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ICompileRequestModel } from '../models/compileRequestModel';
import { ICompilationResponseModel } from '../models/compilationResponseModel';

@Injectable({
    providedIn: 'root',
})
export class CompilerService {
    constructor(private http: HttpClient) {}

    compileCode(model: ICompileRequestModel): Observable<ICompilationResponseModel>{
        return this.http.post<ICompilationResponseModel>(`http://localhost:7071/api/Compile`, model)
    }
}


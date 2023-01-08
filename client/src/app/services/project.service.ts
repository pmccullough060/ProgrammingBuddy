import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ISaveProjectRequestModel } from '../models/saveProjectRequestModel';

@Injectable({
    providedIn: 'root',
})
export class ProjectService {
    constructor(private http: HttpClient) {}

    saveProject(model: ISaveProjectRequestModel): Observable<any>{
        return this.http.post<any>(`http://localhost:7071/api/SaveProject`, model)
    }
}


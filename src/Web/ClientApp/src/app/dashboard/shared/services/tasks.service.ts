import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse  } from '@angular/common/http';
import 'rxjs/add/operator/map';
import {Observable} from "rxjs/Rx";
import { Itask } from '../models/ITask';
import { environment } from '../../../../environments/environment';
import { HttpHeaders } from '@angular/common/http';

@Injectable()
export class TasksService {
  private _apiEndPoint = environment.ahsPwoAPI.url + environment.ahsPwoAPI.endPoints.tasks;

  constructor(private _httpClient: HttpClient) { }

  getTasksByProfileId(profileId: number): Observable<Array<Itask>> {
    return this._httpClient.get<Array<Itask>>(`${ this._apiEndPoint + '/' + profileId }`);
  }

  postTask(task) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
      })
    };
    
    return this._httpClient.post<Itask>(`${ this._apiEndPoint }`, JSON.stringify(task), httpOptions)
  }
}


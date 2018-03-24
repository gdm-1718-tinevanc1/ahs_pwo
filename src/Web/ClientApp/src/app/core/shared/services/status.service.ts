import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';
import {Observable} from "rxjs/Rx";
import 'rxjs/operators/map';
import { environment } from '../../../../environments/environment';
import { HttpHeaders } from '@angular/common/http';
import { IStatus } from '../models/istatus';

@Injectable()
export class StatusService {

  private _apiEndPoint = environment.ahsPwoAPI.url + environment.ahsPwoAPI.endPoints.states;

  constructor(public http:Http, private _httpClient: HttpClient) { }

  getStates(): Observable<Array<IStatus>> {
    return this._httpClient.get<Array<IStatus>>(`${ this._apiEndPoint }`);
  }
}



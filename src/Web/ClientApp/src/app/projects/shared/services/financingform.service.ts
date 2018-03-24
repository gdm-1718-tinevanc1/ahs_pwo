import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse  } from '@angular/common/http';
import {Observable} from "rxjs/Rx";
import { Ifinancingform } from '../models/IFinancingform';
import { environment } from '../../../../environments/environment';
import { HttpHeaders } from '@angular/common/http';

@Injectable()
export class FinancingformService {
  private _apiEndPoint = environment.ahsPwoAPI.url + environment.ahsPwoAPI.endPoints.financingforms;

  constructor( private _httpClient: HttpClient) { }

  postFinancingform(financingform) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
      })
    };
    
    return this._httpClient.post<Ifinancingform>(`${ this._apiEndPoint }`, JSON.stringify(financingform), httpOptions)

  }
}

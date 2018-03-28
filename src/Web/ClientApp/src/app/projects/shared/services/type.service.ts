import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable} from "rxjs/Rx";
import { Ifinancingform } from '../models/Ifinancingform';
import { Ilink } from '../models/Ilink';
import { Iparticipant } from '../models/Iparticipant';
import { environment } from '../../../../environments/environment';


@Injectable()
export class TypeService {
  private _apiEndPointFinancingforms = environment.ahsPwoAPI.url + environment.ahsPwoAPI.endPoints.financingforms;
  private _apiEndPointParticipants = environment.ahsPwoAPI.url + environment.ahsPwoAPI.endPoints.participants;
  private _apiEndPointParticipantsFilter = environment.ahsPwoAPI.url + environment.ahsPwoAPI.endPoints.participantsFilter;
  private _apiEndPointLinks = environment.ahsPwoAPI.url + environment.ahsPwoAPI.endPoints.links;
  
  constructor(private _httpClient: HttpClient) { }

  getFinancingforms(): Observable<Array<Ifinancingform>> {
    return this._httpClient.get<Array<Ifinancingform>>(`${ this._apiEndPointFinancingforms }`);
  }

  getLinks(): Observable<Array<Ilink>> {
    return this._httpClient.get<Array<Ilink>>(`${ this._apiEndPointLinks }`);
  }


  getParticipants(): Observable<Array<Iparticipant>> {
    return this._httpClient.get<Array<Iparticipant>>(`${ this._apiEndPointParticipants }`);
  }

  getParticipantsByType(type_id: number): Observable<Array<Iparticipant>> {
    return this._httpClient.get<Array<Iparticipant>>(`${ this._apiEndPointParticipantsFilter + '/' + type_id }`);
  }
}

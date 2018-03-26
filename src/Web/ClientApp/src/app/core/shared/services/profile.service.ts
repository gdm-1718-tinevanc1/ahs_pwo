import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { HttpClient } from '@angular/common/http';
import 'rxjs/add/operator/map';
import {Observable} from "rxjs/Rx";
import 'rxjs/operators/map';
import { Iprofile } from '../models/Iprofile';
import { environment } from '../../../../environments/environment';
import { HttpHeaders } from '@angular/common/http';

@Injectable()
export class ProfileService {

  private _apiEndPoint = environment.ahsPwoAPI.url + environment.ahsPwoAPI.endPoints.profiles;

  constructor(public http:Http, private _httpClient: HttpClient) { }

  getProfiles(): Observable<Array<Iprofile>> {
    return this._httpClient.get<Array<Iprofile>>(`${ this._apiEndPoint }`);
  }

  getProfileById(id: number) {
    return this._httpClient.get<Iprofile>(`${ this._apiEndPoint + '/' + id + '?withChildren=true'}`)
      //.mapTo(projects => projects.find(project => project.id = id));
  }


  /* saveProfiles (profile){
    if (profile.id) {
      return this.put(profile);
    } 
  }
*/

  postProfile(profile) {
  let httpOptions = new Headers({
    'Content-Type': 'application/json'
  });
    
    return this._httpClient.post<Iprofile>(`${ this._apiEndPoint}`, profile )
  }

  updateProfile(profile) {
    let httpOptions = new Headers({
      'Content-Type': 'application/json'
    });
    
    return this._httpClient.put<Iprofile>(`${ this._apiEndPoint + '/' + profile.id }`, profile )
  }

}


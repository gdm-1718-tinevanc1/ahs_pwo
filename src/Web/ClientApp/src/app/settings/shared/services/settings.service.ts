import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Isetting } from '../models/Isetting';
import { environment } from '../../../../environments/environment';
import { HttpHeaders } from '@angular/common/http';

@Injectable()
export class SettingsService {
  private _apiEndPoint = environment.ahsPwoAPI.url + environment.ahsPwoAPI.endPoints.settings;

  constructor(private _httpClient: HttpClient) { }

  getSetting(id: number) {
    return this._httpClient.get<Isetting>(`${ this._apiEndPoint + '/' + id + '?withChildren=true'}`)
  }

  saveSettings (setting){
    if (setting.id) {
      return this.put(setting);
    } 
    return this.post(setting);
  }

  private post(setting) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
      })
    };
    return this._httpClient.post<Isetting>(`${ this._apiEndPoint }`, JSON.stringify(setting), httpOptions)
  }

  private put(setting) {
    let httpOptions = new Headers({
      'Content-Type': 'application/json'
    });
    return this._httpClient.put<Isetting>(`${ this._apiEndPoint + '/' + setting.id }`, setting )

  }

}


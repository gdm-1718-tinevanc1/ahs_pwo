import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { HttpClient, HttpErrorResponse  } from '@angular/common/http';
import 'rxjs/add/operator/map';
import {Observable} from "rxjs/Rx";
import 'rxjs/operators/map';
import { Iproject } from '../models/IProject';
import { environment } from '../../../../environments/environment';
import { ErrorObservable } from 'rxjs/observable/ErrorObservable';
import { catchError, retry } from 'rxjs/operators';
import { HttpHeaders } from '@angular/common/http';


@Injectable()
export class ProjectService {
  private _apiEndPoint = environment.ahsPwoAPI.url + environment.ahsPwoAPI.endPoints.projects;
  private _apiEndPointByStatus = environment.ahsPwoAPI.url + environment.ahsPwoAPI.endPoints.projectsStatusfilter;

  private _apiEndPointGeneral = environment.ahsPwoAPI.url + environment.ahsPwoAPI.endPoints.projectsGeneral;
  private _apiEndPointBudget = environment.ahsPwoAPI.url + environment.ahsPwoAPI.endPoints.projectsBudget;
  private _apiEndPointMedia = environment.ahsPwoAPI.url + environment.ahsPwoAPI.endPoints.projectsMedia;
  private _apiEndPointMetadata = environment.ahsPwoAPI.url + environment.ahsPwoAPI.endPoints.projectsMetadata;

  private _apiEndPointProjectfile = environment.ahsPwoAPI.url + environment.ahsPwoAPI.endPoints.projectfile;
  private _apiEndPointProjectsheet = environment.ahsPwoAPI.url + environment.ahsPwoAPI.endPoints.projectsheet;

  constructor( public http:Http, private _httpClient: HttpClient) {}


  getProjects(): Observable<Array<Iproject>> {
    return this._httpClient.get<Array<Iproject>>(`${ this._apiEndPoint + '?withChildren=true' }`);
  }

  getProjectById(id: number) {
    return this._httpClient.get<Iproject>(`${ this._apiEndPoint + '/' + id + '?withChildren=true'}`)
      //.mapTo(projects => projects.find(project => project.id = id));
  }

  getProjectGeneralById(id: number) {
    return this._httpClient.get<Iproject>(`${ this._apiEndPointGeneral + '/' + id}`)
  }

  getProjectBudgetById(id: number) {
    return this._httpClient.get<Iproject>(`${ this._apiEndPointBudget + '/' + id}`)
  }

  getProjectMediaById(id: number) {
    return this._httpClient.get<Iproject>(`${ this._apiEndPointMedia + '/' + id}`)
  }

  getProjectMetadataById(id: number) {
    return this._httpClient.get<Iproject>(`${ this._apiEndPointMetadata + '/' + id}`)
  }

  getProjectByStatus(id: number) {
    return this._httpClient.get<Array<Iproject>>(`${ this._apiEndPointByStatus + '/' + id}`)
  }

  getProjectfile(id: number): Observable<Blob> {
    return this._httpClient.get(`${ this._apiEndPointProjectfile + '/' + id}`, {
      responseType: "blob"
    })
  }

  getProjectsheet(id: number): Observable<Blob> {
    return this._httpClient.get(`${ this._apiEndPointProjectsheet + '/' + id}`, {
      responseType: "blob"
    })
  }



  saveProjects (project, type){
    if (project.id) {
      switch(type){
        case 'general': 
          return this.putGeneral(project);
        case 'budget': 
          return this.putBudget(project);
        case 'media': 
          return this.putMedia(project);
        case 'metadata': 
          return this.putMetadata(project);
        default: 
          return this.put(project);
      }
    } 
    return this.post(project)
  }

  private post(project) {
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type':  'application/json',
      })
    };
    
    return this._httpClient.post<Iproject>(`${ this._apiEndPoint }`, JSON.stringify(project), httpOptions)

  }

  private put(project) {
    let httpOptions = new Headers({
      'Content-Type': 'application/json'
    });
    
    return this._httpClient.put<Iproject>(`${ this._apiEndPoint + '/' + project.id }`, project )

  }

  private putGeneral(project) {
    let httpOptions = new Headers({
      'Content-Type': 'application/json'
    });
    
    return this._httpClient.put<Iproject>(`${ this._apiEndPointGeneral + '/' + project.id }`, project )
  }

  private putBudget(project) {
    let httpOptions = new Headers({
      'Content-Type': 'application/json'
    });
    
    return this._httpClient.put<Iproject>(`${ this._apiEndPointBudget + '/' + project.id }`, project )
  }

  private putMedia(project) {
    let httpOptions = new Headers({
      'Content-Type': 'application/json'
    });
    
    return this._httpClient.put<Iproject>(`${ this._apiEndPointMedia + '/' + project.id }`, project )
  }

  private putMetadata(project) {
    let httpOptions = new Headers({
      'Content-Type': 'application/json'
    });
    
    return this._httpClient.put<Iproject>(`${ this._apiEndPointMetadata + '/' + project.id }`, project )
  }


  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    } else {
      console.error(
        `Backend returned code ${error.status}, ` +
        `body was: ${error.error}`);
    }
    return new ErrorObservable(
      'Something bad happened; please try again later.');
  };

}

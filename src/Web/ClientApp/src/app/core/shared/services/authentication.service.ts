import { Injectable } from '@angular/core';

@Injectable()
export class AuthenticationService {
  public isAdmin = false;
  public profileId = 1;
  constructor() { }

}

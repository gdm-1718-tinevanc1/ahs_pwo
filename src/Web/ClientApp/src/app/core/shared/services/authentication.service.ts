import { Injectable } from '@angular/core';

@Injectable()
export class AuthenticationService {
  public isAdmin = true;
  public profileId = 1;
  constructor() { }

}

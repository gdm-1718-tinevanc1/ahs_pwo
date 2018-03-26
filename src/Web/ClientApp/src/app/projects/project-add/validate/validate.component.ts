import { Component, OnInit, Input, AfterViewInit, ViewChildren } from '@angular/core';
import { AuthenticationService } from '../../../core/shared/services/authentication.service'
import { ProfileService } from '../../../core/shared/services/profile.service'
import { Iprofile } from '../../../core/shared/models/Iprofile';

@Component({
  selector: 'app-validate',
  templateUrl: './validate.component.html',
  styleUrls: ['./validate.component.scss']
})
export class ValidateComponent implements OnInit  {
  isAdmin = this.authenticationService.isAdmin;
  validate = {}
  private _projectCredentials: {};
  profiles: Array<Iprofile>;
  validatedBy;
  @Input() set projectCredentials(value: Object) {
    this.validate = value
  }

  constructor(
    private authenticationService:AuthenticationService,
    private profileService:ProfileService
  ) { }


  ngOnInit(){
    // this.validate.validated_by = this.authenticationService.profileId;
    this.profileService.getProfiles().subscribe(
      result => { this.profiles = result },
      err => console.log('err')
    )
    this.profileService.getProfileById(this.authenticationService.profileId).subscribe(
      result => { this.validatedBy = result.userName },
      err => console.log('err')
    )
  }
}

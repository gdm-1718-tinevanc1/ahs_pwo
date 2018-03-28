import { Component, OnInit } from '@angular/core';
import { PageTitleService } from '../../core/shared/services/page-title.service';
import { TranslateService } from '@ngx-translate/core';
import { ProfileService } from '../../core/shared/services/profile.service'
import { Iprofile } from '../../core/shared/models/Iprofile';

@Component({
  selector: 'app-users-add',
  templateUrl: './users-add.component.html',
  styleUrls: ['./users-add.component.scss']
})
export class UsersAddComponent implements OnInit {
  message = {
    succes: '',
    error: ''
  }
  credentials = {
    mail: null,
    userName: null,
    firstName: null,
    lastName: null,
    image: null
  }
  users = [
    {
      id: 1,
      mail: 'test1@test.be',
      userName: "test1",
      firstName: "Test1",
      lastName: "Test1",
      image: "http://lorempixel.com/640/480/"
    },
    {
      id: 2,
      mail: 'test2@test.be',
      userName: "test2",
      firstName: "Test2",
      lastName: "Test2",
      image: "http://lorempixel.com/640/480/"
    },
    {
      id: 3,
      mail: 'test3@test.be',
      userName: "test3",
      firstName: "Test3",
      lastName: "Test3",
      image: "http://lorempixel.com/640/480/"
    }
  ]

  constructor(
    private pageTitleService: PageTitleService,
    private translate: TranslateService,
    private profileService:ProfileService
  ) { }

  ngOnInit() {
    this.translate.get('Nieuwe gebruiker').subscribe((res: string) => {
      this.pageTitleService.setTitle(res)
    });

    this.pageTitleService.setSubTitle("")
  }

  save() {
    let credentials = {
      email: this.credentials.mail,
      userName: this.credentials.userName,
      firstName: this.credentials.firstName,
      lastName: this.credentials.lastName,
      image: this.credentials.image
    }

    this.profileService.postProfile(credentials).subscribe(
      res => {
        this.message.error =  '';
        this.message.succes =  "Uw wijzigingen zijn opgeslaan"
      },
      err => {
        console.log("Error occured");
        this.message.succes = '';
        this.message.error = "Uw wijzigingen zijn niet opgeslaan, gelieve opnieuw te proberen" }
    );
  }

}

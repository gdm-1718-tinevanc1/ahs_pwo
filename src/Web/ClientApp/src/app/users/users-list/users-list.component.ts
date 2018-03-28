import { Component, OnInit } from '@angular/core';
import { ProfileService } from '../../core/shared/services/profile.service'
import { Http } from '@angular/http';
import { PageTitleService } from '../../core/shared/services/page-title.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.scss']
})
export class UsersListComponent implements OnInit {
  searchText;
  p;
  profiles = [];
  dropdownExpaned: boolean = false;
  filterSelectRol1: boolean = false;
  filterSelectRol2: boolean = false;
  filterSelectRol3: boolean = false;
  filterSelectAll: boolean = false;
  filterDisabledCheckboxs = false;

  selectRol1 = [];
  selectRol2 = [];
  selectRol3 = [];
  selectAll = [];
  disabledCheckboxs = [];

  constructor(
    private profileService:ProfileService, public http:Http,
    private pageTitleService: PageTitleService,
    private translate: TranslateService) { }

  ngOnInit() {
    this.translate.get('Gebruikers').subscribe((res: string) => {
      this.pageTitleService.setTitle(res)
    });

    this.pageTitleService.setSubTitle("")
    this.profileService.getProfiles().subscribe(
      result => { 
        this.profiles = result },
      err => console.log('err')
    )
  }

  toggleAllCheckbox(index){
    if(this.selectAll[index] == false){
      this.selectRol1[index] = this.selectRol2[index] = this.selectRol3[index] = this.selectAll[index] = this.disabledCheckboxs[index] = false;
    } else {
      this.selectRol1[index] = this.selectRol2[index] = this.selectRol3[index] = this.selectAll[index] = this.disabledCheckboxs[index] = true;
    }
  }
  toggleFilterAllCheckbox(){
    if(this.filterSelectAll == false){
      this.filterSelectRol1 = this.filterSelectRol2 = this.filterSelectRol3 = this.filterSelectAll = this.filterDisabledCheckboxs = false;
    } else {
      this.filterSelectRol1 = this.filterSelectRol2 = this.filterSelectRol3 = this.filterSelectAll = this.filterDisabledCheckboxs = true;
    }

  }

  searchBy($event){

  }


}


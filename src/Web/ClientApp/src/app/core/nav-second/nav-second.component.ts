import { Component, OnInit } from '@angular/core';
import { SharedService } from '../shared/services/shared.service'

@Component({
  selector: 'app-nav-second',
  templateUrl: './nav-second.component.html',
  styleUrls: ['./nav-second.component.scss']
})
export class NavSecondComponent implements OnInit {
  selectedLanguage = 'nl';
  constructor(private sharedService:SharedService) { }

  ngOnInit() {
    this.sharedService.setLanguage(this.selectedLanguage)
  }

  onChange($event){
    this.sharedService.setLanguage($event)
  }
}

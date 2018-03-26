import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../../core/shared/services/project.service'
import { Iproject } from '../../core/shared/models/Iproject';
import { StatusService } from '../../core/shared/services/status.service'

@Component({
  selector: 'app-projects-list',
  templateUrl: './projects-list.component.html',
  styleUrls: ['./projects-list.component.scss']
})
export class ProjectsListComponent implements OnInit {
  searchText;
  p;
  dropdownExpaned: boolean = false;
  projects: Array<Iproject>; // Array<Iproject>
  states: {};
  statusValue = null;
  filterSelectRol1: boolean = false;
  filterSelectRol2: boolean = false;
  filterSelectRol3: boolean = false;
  filterSelectRol4: boolean = false;
  filterSelectAll: boolean = false;
  filterDisabledCheckboxs = false;

  constructor(
    private projectService:ProjectService,
    private statusService:StatusService,
  ) { }

  ngOnInit() {
    this.statusService.getStates().subscribe(
      states => {
        this.states = states;
    })
    this.projectService.getProjects().subscribe(
      result => { 
        this.projects = result
        for(var i = 0; i < this.projects.length; i++){
          this.projects[i].shorttitle = JSON.parse(this.projects[i].shorttitle.toString());
        }
      },
      err => console.log('err')
    )
  }

  toggleFilterAllCheckbox(){
    if(this.filterSelectAll == false){
      this.filterSelectRol1 = this.filterSelectRol2 = this.filterSelectRol3 = this.filterSelectRol4 = this.filterSelectAll = this.filterDisabledCheckboxs = false;
    } else {
      this.filterSelectRol1 = this.filterSelectRol2 = this.filterSelectRol3 = this.filterSelectRol4 = this.filterSelectAll = this.filterDisabledCheckboxs = true;
    }

  }

}

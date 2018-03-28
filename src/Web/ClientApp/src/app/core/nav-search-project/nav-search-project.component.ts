import { Component, OnInit, Input } from '@angular/core';
import { ProjectService } from '../shared/services/project.service'
import { Http } from '@angular/http';
import { Iproject } from '../shared/models/IProject';

@Component({
  selector: 'app-nav-search-project',
  templateUrl: './nav-search-project.component.html',
  styleUrls: ['./nav-search-project.component.scss']
})
export class NavSearchProjectComponent implements OnInit {
  @Input() isExpandedFilter;
  slice_amount = 6;
  slice_start = 0;
  slice_end = this.slice_amount;
  projects: Array<Iproject>;
  projectsList: Array<Iproject>;
  filterValue = '';
  constructor(private projectService:ProjectService, public http:Http) { }

  ngOnInit() {
    this.projectService.getProjects().subscribe(
      result => { 
        this.projects = result
        this.projectsList = result
        for(var i = 0; i < this.projects.length; i++){
          this.projects[i].title = JSON.parse(this.projects[i].title.toString());
          this.projects[i].subtitle = JSON.parse(this.projects[i].subtitle.toString());
          this.projects[i].shorttitle = JSON.parse(this.projects[i].shorttitle.toString());
          this.projects[i].description = JSON.parse(this.projects[i].description.toString());
        }
      }
    )
  }

  nextProjects() {
    var keys = Object.keys(this.projects);
    var length = keys.length;
    if(length < this.slice_end){
      this.slice_start = 0;
      this.slice_end = this.slice_amount;
    } else {
      this.slice_end = this.slice_end + this.slice_amount;
      this.slice_start = this.slice_start + this.slice_amount;
    }
  }

  hack(val) {
    return Array.from(val);
  }

 filter(event){
    //status.name
  if(event.value == 'financingform'){
    this.projects = this.projectsList;
    let projects =[]; 
    for(var i = 0; i < this.projects.length; i++){
      if(this.projects[i].financingforms[0]){ 
          projects.push(this.projects[i])
      }
    }
    this.projects = projects;
    this.filterValue = 'financingforms[0].financingform.name';
  } else if(event.value == 'education'){
    this.projects = this.projectsList;
    let projects =[]; 
    for(var i = 0; i < this.projects.length; i++){
      if(this.projects[i].participants[0]){
        if(this.projects[i].participants[0].participant.typeParticipant === "Opleiding"){
          projects.push(this.projects[i])
        }
      }
    }
    this.projects = projects;
    this.filterValue = 'participants[0].participant.name';
  } else if(event.value == 'ODC'){
    this.projects = this.projectsList;
    let projects =[]; 
    for(var i = 0; i < this.projects.length; i++){
      if(this.projects[i].participants[0]){
        if(this.projects[i].participants[0].participant.typeParticipant === "ODC"){
          projects.push(this.projects[i])
        }
      }
    }
    this.projects = projects;
    this.filterValue = 'participants[0].participant.name';
  } else{
    this.filterValue = '';
    this.projects = this.projectsList;
  }
 }
}


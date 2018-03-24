import { Component, OnInit } from '@angular/core';
import { ProjectService } from '../../core/shared/services/project.service'
import { Http } from '@angular/http';
import { PageTitleService } from '../../core/shared/services/page-title.service';
import { Iproject } from '../../core/shared/models/Iproject';
import { TranslateService } from '@ngx-translate/core';
import { StatusService } from '../../core/shared/services/status.service'

@Component({
  selector: 'app-project-list',
  templateUrl: './project-list.component.html',
  styleUrls: ['./project-list.component.scss']
})
export class ProjectListComponent implements OnInit {
  dropdownExpaned: boolean = false;
  projects: Array<Iproject>;
  states: {};
  statusValue = null;
  constructor(
    private projectService:ProjectService, 
    public http:Http,
    private pageTitleService: PageTitleService,
    private statusService:StatusService,
    private translate: TranslateService
  ) { }

  ngOnInit() {
    this.statusService.getStates().subscribe(
      states => {
        this.states = states;
    })
    this.translate.get('Projecten').subscribe((res: string) => {
      this.pageTitleService.setTitle(res)
    });
    this.pageTitleService.setSubTitle("")
    this.projectService.getProjects().subscribe(
      result => { 
        this.projects = result
        for(var i = 0; i < this.projects.length; i++){
          this.projects[i].title = JSON.parse(this.projects[i].title.toString());
          this.projects[i].subtitle = JSON.parse(this.projects[i].subtitle.toString());
          this.projects[i].shorttitle = JSON.parse(this.projects[i].shorttitle.toString());
          this.projects[i].description = JSON.parse(this.projects[i].description.toString());
        }
       },
      err => console.log('err')
    )
  }

  /* updateCheckedOptions(option, event) {
    this.test = 'test' + event.source.value;
    // this.selectedFilter = [1,2]
    setTimeout(() => { if(event.source.checked == true){
      // this.selectedFilter.push(parseInt(event.source.value))
      this.selectedFilter = event.source.value
      } else{
        /* var index = this.selectedFilter.indexOf(parseInt(event.source.value));
        if (index > -1) {
          this.selectedFilter.splice(index, 1);
        } */
      /*   this.selectedFilter = null
      }
      console.log(this.selectedFilter)
      alert(this.selectedFilter)
    })
  } */
}

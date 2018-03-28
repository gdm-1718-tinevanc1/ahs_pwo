import { Component, OnInit } from '@angular/core';
import { PageTitleService } from '../core/shared/services/page-title.service';
import { ProjectService } from '../core/shared/services/project.service';
import { ProfileService } from '../core/shared/services/profile.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {
  message = {
    succes: '',
    error: ''
  };

  countProjects: number;
  countProfiles: number;
  countValidateProjects: number;
 

  constructor(
    private pageTitleService: PageTitleService,
    private projectService: ProjectService,
    private profileService: ProfileService) { }

  ngOnInit() {
    this.pageTitleService.setTitle("Dashboard")
    this.pageTitleService.setSubTitle("")

    this.projectService.getProjects().subscribe(
          result => { this.countProjects = result.length }
      )

    this.profileService.getProfiles().subscribe(
        result => { this.countProfiles = result.length }
    )

    this.projectService.getProjectByStatus(3).subscribe(
      result => { this.countValidateProjects = result.length }
  )
  }
}

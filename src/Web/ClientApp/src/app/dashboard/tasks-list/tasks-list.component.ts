import { Component, OnInit } from '@angular/core';
import { TasksService } from '../shared/services/tasks.service';
import { ProjectService } from '../../core/shared/services/project.service'
import { Itask } from '../shared/models/Itask';
import { Iproject } from '../../core/shared/models/IProject';
import { AuthenticationService } from '../../core/shared/services/authentication.service'

@Component({
  selector: 'app-tasks-list',
  templateUrl: './tasks-list.component.html',
  styleUrls: ['./tasks-list.component.scss']
})
export class TasksListComponent implements OnInit {
  message = {
    succes: '',
    error: ''
  };
  profileId = this.authenticationService.profileId;
  taskCredentials = {
    name: '',
    projectId: null,
    profileId: this.profileId
  }
  tasks: Array<Itask>;
  projects: Array<Iproject>

  constructor(
    private tasksService: TasksService,
    private projectService:ProjectService,
    private authenticationService:AuthenticationService
  ) { }

  ngOnInit() {
    this.getTasks();
    this.projectService.getProjects().subscribe(
      result => { 
        this.projects = result
        for(var i = 0; i < this.projects.length; i++){
          this.projects[i].shorttitle = JSON.parse(this.projects[i].shorttitle);
        }

       },
      err => console.log('err')
    )
  }

  getTasks() {
    this.tasksService.getTasksByProfileId(this.profileId).subscribe(
      result => { this.tasks = result},
      err => console.log('err')
    )
  
  }
  
  save() {
    this.tasksService.postTask(this.taskCredentials).subscribe(
      res => {
        this.getTasks()
      },
      err => {
        console.log("Error occured");
        this.message.error = "Uw todo is niet opgeslaan, gelieve opnieuw te proberen"
      }
    );
  }

}


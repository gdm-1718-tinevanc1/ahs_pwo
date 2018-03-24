import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from '../app-routing.module';
import { DashboardComponent } from './dashboard.component';
import { MaterialModule } from '../core/material/material.module';
import { TasksService } from './shared/services/tasks.service';
import { NotificationsListComponent } from './notifications-list/notifications-list.component';
import { ProjectsListComponent } from './projects-list/projects-list.component';
import { TasksListComponent } from './tasks-list/tasks-list.component';
import { SharedModule } from '../shared/shared.module';
import { NgxPaginationModule } from 'ngx-pagination';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    AppRoutingModule,
    SharedModule,
    NgxPaginationModule
  ],
  declarations: [
    DashboardComponent,
    NotificationsListComponent,
    ProjectsListComponent,
    TasksListComponent
  ],
  providers: [TasksService]
})
export class DashboardModule { }

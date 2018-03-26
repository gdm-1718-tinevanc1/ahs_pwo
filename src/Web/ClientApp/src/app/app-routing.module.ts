import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';


import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard/dashboard.component';

import { SettingsComponent } from './settings/settings.component'
import { UsersListComponent} from './users/users-list/users-list.component'
import { UsersAddComponent } from './users/users-add/users-add.component'

import { ProjectListComponent } from './projects/project-list/project-list.component'
import { ProjectAddComponent } from './projects/project-add/project-add.component'

import { FormGeneralComponent } from './projects/project-add/form-general/form-general.component'
import { FormAbstractComponent } from './projects/project-add/form-abstract/form-abstract.component'
import { FormMetadataComponent } from './projects/project-add/form-metadata/form-metadata.component'
import { FormBudgetComponent } from './projects/project-add/form-budget/form-budget.component'
import { FormMediaComponent } from './projects/project-add/form-media/form-media.component'
import { FormDownloadComponent } from './projects/project-add/form-download/form-download.component'

const children_project = [
  {path: '', redirectTo:'general', pathMatch: 'full'},
  {path: 'general', component:FormGeneralComponent},
  {path: 'abstract', component:FormAbstractComponent},
  {path: 'metadata', component:FormMetadataComponent},
  {path: 'budget', component:FormBudgetComponent},
  {path: 'media', component:FormMediaComponent},
  {path: 'downloads', component:FormDownloadComponent}
]

const routes: Routes = [
  { path: '', component: DashboardComponent, pathMatch: 'full' },
  { path: 'projects', component:ProjectListComponent},
  { path: 'project', component:ProjectAddComponent, children: children_project},
  { path: 'project/:id', component:ProjectAddComponent, children: children_project},
  { path: 'user', component:UsersAddComponent},
  { path: 'settings', component:SettingsComponent, data: { title: 'Settings' }},
  { path: 'profiles', component:UsersListComponent}
];


@NgModule({
  imports: [RouterModule.forRoot(routes, {useHash: true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }

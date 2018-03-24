import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { NavSecondComponent } from './nav-second/nav-second.component';
import { NavSearchProjectComponent } from './nav-search-project/nav-search-project.component';
import { MaterialModule } from './material/material.module'
import { AppRoutingModule } from './../app-routing.module';
import { ProjectService } from './shared/services/project.service';
import { HeaderComponent } from './header/header.component';
import { PageTitleService } from './shared/services/page-title.service';
import { ProfileService } from './shared/services/profile.service';
import { SharedModule } from '../shared/shared.module';
import { AuthenticationService } from './shared/services/authentication.service';
import { SharedService } from './shared/services/shared.service';
import { StatusService } from './shared/services/status.service'
import { FormsModule } from '@angular/forms';

@NgModule({
  imports: [
    CommonModule,
    MaterialModule,
    AppRoutingModule,
    SharedModule, 
    FormsModule,
  ],
  declarations: [
    NavMenuComponent, 
    NavSecondComponent, 
    NavSearchProjectComponent, 
    HeaderComponent
  ],
  exports: [
    NavMenuComponent, 
    NavSecondComponent, 
    NavSearchProjectComponent,
    HeaderComponent,
  ],
  providers: [ProjectService, PageTitleService, ProfileService, AuthenticationService, SharedService, StatusService]
})
export class CoreModule { }


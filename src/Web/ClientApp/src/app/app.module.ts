import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http'; 
import { HttpModule } from '@angular/http';
import { MaterialModule } from './core/material/material.module';
import { AppRoutingModule } from './app-routing.module';
import { AngularFontAwesomeModule } from 'angular-font-awesome';
import { NgxPaginationModule } from 'ngx-pagination';

/* Components */
import { AppComponent } from './app.component';

/* Modules */
import { CoreModule } from './core/core.module';
import { ProjectsModule } from './projects/projects.module';
import { UsersModule } from './users/users.module';
import { SettingsModule } from './settings/settings.module';
import { DashboardModule } from './dashboard/dashboard.module';
import { SharedModule } from './shared/shared.module';

/* pipes */
import { SearchByPipe } from './shared/pipes/search-by.pipe';
import { FilterByStatusPipe } from './shared/pipes/filter-by-status.pipe';
import { OrderByPipe } from './shared/pipes/order-by.pipe';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    AppRoutingModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    HttpModule,
    FormsModule,
    CoreModule,
    MaterialModule,
    ProjectsModule,
    UsersModule,
    SettingsModule,
    DashboardModule,
    SharedModule,
    AngularFontAwesomeModule,
    NgxPaginationModule
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }

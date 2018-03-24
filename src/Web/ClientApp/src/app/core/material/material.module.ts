import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

/* Material */
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {MatButtonModule, MatCheckboxModule, MatFormFieldModule, MatInputModule, MatSelectModule} from '@angular/material';
import {MatDatepickerModule} from '@angular/material/datepicker';
import {MatNativeDateModule} from '@angular/material'
import {MatGridListModule} from '@angular/material/grid-list';
import {MatChipsModule} from '@angular/material/chips';
import {MatIconModule} from '@angular/material/icon';
import {MatSliderModule} from '@angular/material/slider';
import {MatSidenavModule} from '@angular/material/sidenav';
import {MatExpansionModule} from '@angular/material/expansion';
import {MatTableModule} from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material';
import {MatRadioModule} from '@angular/material/radio';
import {MatCardModule} from '@angular/material/card';

@NgModule({
  imports: [
    CommonModule,
    BrowserAnimationsModule,
    MatButtonModule, 
    MatCheckboxModule,
    MatFormFieldModule, 
    MatInputModule, 
    MatSelectModule,
    MatDatepickerModule, 
    MatNativeDateModule, 
    MatGridListModule, 
    MatChipsModule, 
    MatIconModule,
    MatSliderModule,
    MatSidenavModule,
    MatTableModule,
    MatPaginatorModule,
    MatRadioModule,
    MatCardModule
  ],
  exports: [
    CommonModule,
    BrowserAnimationsModule,
    MatButtonModule, 
    MatCheckboxModule,
    MatFormFieldModule, 
    MatInputModule, 
    MatSelectModule,
    MatDatepickerModule, 
    MatNativeDateModule, 
    MatGridListModule, 
    MatChipsModule, 
    MatIconModule,
    MatSliderModule,
    MatSidenavModule,
    MatExpansionModule,
    MatTableModule,
    MatPaginatorModule,
    MatRadioModule,
    MatCardModule
  ],
  declarations: []
})
export class MaterialModule { }

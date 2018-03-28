import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient } from '@angular/common/http';

/* pipes */
import { SearchByPipe } from './pipes/search-by.pipe';
import { SearchByMediatypePipe } from './pipes/search-by-mediatype.pipe';
import { FilterByStatusPipe } from './pipes/filter-by-status.pipe';
import { OrderByPipe } from './pipes/order-by.pipe';
import { TransModule } from './pipes/translate/trans.module';

@NgModule({
  imports: [
    CommonModule,
    TransModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
  ],
  declarations: [
    SearchByPipe,
    SearchByMediatypePipe,
    FilterByStatusPipe,
    OrderByPipe,
  ],
  exports: [
    SearchByPipe,
    SearchByMediatypePipe,
    FilterByStatusPipe,
    OrderByPipe,
    TranslateModule
  ],
  providers: [],
})
export class SharedModule { }

export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
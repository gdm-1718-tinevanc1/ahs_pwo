import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'searchByMediatype'
})
export class SearchByMediatypePipe implements PipeTransform {

  transform(items: any[], args?: any): any {
    return items.filter(item => item.typeMedia == args);
  }

}

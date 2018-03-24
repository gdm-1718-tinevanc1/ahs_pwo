import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filterByStatus'
})
export class FilterByStatusPipe implements PipeTransform {

  transform(items: any[], args: any): any {
    if (args == null) return items;
    return items.filter(function(item){
        return item.statusId == args;
    })
  }

}

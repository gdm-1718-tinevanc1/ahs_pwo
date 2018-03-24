import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'searchBy'
})
export class SearchByPipe implements PipeTransform {

   transform(items: any, term: string): any {
    if (!term || !items) return items;

    return SearchByPipe.filter(items, term);
  }

  /**
   * 
   * @param items List of items to filter
   * @param term  a string term to compare with every property of the list
   * 
   */
  static filter(items: Array<{ [key: string]: any }>, term: string): Array<{ [key: string]: any }> {

    const toCompare = term.toLowerCase();

    return items.filter(function (item: any) {
      for (let property in item) {
        if (item[property] === null) {
          continue;
        }
        if (item[property].toString().toLowerCase().includes(toCompare)) {
          return true;
        }
      }
      return false;
    });
  }

}


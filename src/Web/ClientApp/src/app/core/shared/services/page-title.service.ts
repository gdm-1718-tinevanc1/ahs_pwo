import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

@Injectable()
export class PageTitleService {
  public title: BehaviorSubject<string> = new BehaviorSubject<string>(null);
  public subtitle: BehaviorSubject<string> = new BehaviorSubject<string>(null);

  setTitle(value: string) {
      this.title.next(value);
  }

  setSubTitle(value: string) {
    this.subtitle.next(value);
}

}


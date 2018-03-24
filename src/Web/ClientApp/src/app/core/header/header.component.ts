import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { PageTitleService } from '../shared/services/page-title.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {
  title: string;
  subtitle: string;
  constructor(private pageTitleService: PageTitleService) { }
  ngOnInit() {
    this.pageTitleService.title.subscribe((val: string) => {
      this.title = val;
    });
    this.pageTitleService.subtitle.subscribe((val: string) => {
      this.subtitle = val;
    });
  }
}

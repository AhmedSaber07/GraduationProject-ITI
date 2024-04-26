import {TranslateModule, TranslateService } from '@ngx-translate/core';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, NavigationEnd, Router, RouterOutlet } from '@angular/router';
import { FooterComponent } from './Shared/Components/footer/footer.component';
import { HeaderComponent } from './Shared/Components/header/header.component';
import { HttpClient } from '@angular/common/http';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { DataSharingService } from './Shared/Services/data-sharing.service';

export function HttpLoaderFactory(http:HttpClient){
  return new TranslateHttpLoader(http);
  }

  @Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,HeaderComponent,FooterComponent,TranslateModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent implements OnInit {
  title = '2B-ECommerce';
  direction: string = 'rtl'; // Default direction
  
  showHeader: boolean = true;
  showFooter: boolean = true;

  constructor(private _dataSharingService: DataSharingService,private router: Router, private activatedRoute: ActivatedRoute)
  {
    
  }

  ngOnInit(): void {
    this._dataSharingService.languageChanged.subscribe(lang => {
      this.direction = lang === 'ar' ? 'rtl' : 'ltr';
    });
    


    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        const firstChild = this.activatedRoute.firstChild;
        if (firstChild && firstChild.snapshot.routeConfig?.path === '**') {
          this.showHeader = false;
          this.showFooter = false;
        } else {
          this.showHeader = true;
          this.showFooter = true;
        }
      }
    });

}
}
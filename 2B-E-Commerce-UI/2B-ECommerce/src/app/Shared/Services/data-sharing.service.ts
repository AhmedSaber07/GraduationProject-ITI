import { EventEmitter, Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { BehaviorSubject, Observable, map, take, timer } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DataSharingService {

  private languageSubject = new BehaviorSubject<string>('ar');
  language$ = this.languageSubject.asObservable();

  private countdownTimer$: Observable<number>;
  languageChanged: EventEmitter<string> = new EventEmitter<string>(); 
  private categoryIdInHeader = new BehaviorSubject<string>("");
  private searchQuerySubject: BehaviorSubject<string> = new BehaviorSubject<string>('');
  public searchQuery: Observable<string> = this.searchQuerySubject.asObservable();
  currentCategoryId = this.categoryIdInHeader.asObservable();

  constructor(private translateService: TranslateService) { 
    this.countdownTimer$ = timer(0, 1000);
    const storedLang = localStorage.getItem('lang');
    const defaultLang = storedLang || 'ar'; 
    this.translateService.use(defaultLang);
    document.dir = defaultLang === 'ar' ? 'rtl' : 'ltr'; 
    this.emitLanguageChange(defaultLang); 
  }

  startTimer(duration: number): Observable<number> {
    return this.countdownTimer$.pipe(
      take(duration + 1), 
      map(time => duration - time)
    );
  }

setLanguage(lang: string) {
  this.translateService.use(lang);
  localStorage.setItem('lang', lang);
  document.dir = lang === 'ar' ? 'rtl' : 'ltr';
  this.languageSubject.next(lang); 
    this.emitLanguageChange(lang);
}

private emitLanguageChange(lang: string) {
  this.languageChanged.emit(lang);
}


  changeCategoryId(categoryId: string) {
    if (categoryId !== null) {
      this.categoryIdInHeader.next(categoryId);
    }
  }

  setSearchQuery(query: string): void {
    this.searchQuerySubject.next(query);
  }

}

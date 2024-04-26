import { DatePipe } from '@angular/common';
import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dateFormat',
  standalone: true
})
export class DateFormatPipe implements PipeTransform {

  transform(value: string): string {
    const date = new Date(value);
    return new DatePipe('en-US').transform(date, 'yyyy-MM-dd') ?? "";
  }

}

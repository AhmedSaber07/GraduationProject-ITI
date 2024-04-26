import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'customDisplayPrice',
  standalone: true
})
export class CustomDisplayPricePipe implements PipeTransform {

  transform(value: number): string {
    return value.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ',');
  }

}

import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'diplayProductDescription',
  standalone: true
})
export class DiplayProductDescriptionPipe implements PipeTransform {

  transform(value: string): string {
    if (!value) {
      return '';
    }
    let details = value.split('$');
    return details.join('\n\n');
  }

}

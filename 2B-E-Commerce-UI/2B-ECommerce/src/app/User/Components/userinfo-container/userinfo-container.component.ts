import { Component } from '@angular/core';
import { UserProfileComponent } from '../UserProfile/user-profile/user-profile.component';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-userinfo-container',
  standalone: true,
  imports: [UserProfileComponent,CommonModule,RouterModule],
  templateUrl: './userinfo-container.component.html',
  styleUrl: './userinfo-container.component.css'
})
export class UserinfoContainerComponent {

}

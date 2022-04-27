import { Component, OnInit } from '@angular/core';
import { User } from 'oidc-client';
import { AuthService } from 'src/app/shared/services/auth.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  public openDashboard: boolean = false;
  public user : User;
  constructor(public _authService: AuthService) {}

  ngOnInit(): void {
    this._authService.getUserAsync()
        .then(u => {
          this.user = u;
        });
  }

  ToggleDashboard() {
    this.openDashboard = !this.openDashboard;
  }

}

import { Component, OnInit } from '@angular/core';
import { User } from 'oidc-client';
import { Observable } from 'rxjs';
import { Orders } from 'src/app/shared/classes/Orders';
import { AuthService } from 'src/app/shared/services/auth.service';
import { OrderService } from 'src/app/shared/services/order.service';

@Component({
  selector: 'app-orderhistory',
  templateUrl: './orderhistory.component.html',
  styleUrls: ['./orderhistory.component.scss']
})
export class OrderHistoryComponent implements OnInit {

  public openDashboard: boolean = false;
  public orderCancelled: boolean = false;
  public user: User;
  public orders$: Observable<Orders[]>;
  constructor(public orderService: OrderService) { }

  ngOnInit(): void {
    this.orders$ = this.orderService.orders;
  }

  cancelOrder(id:number) {
    this.orderService.cancel(id).subscribe(o=> this.orderCancelled = true);
  }
  ToggleDashboard() {
    this.openDashboard = !this.openDashboard;
  }

}

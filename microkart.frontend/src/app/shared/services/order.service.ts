import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Orders } from '../classes/Orders';

const state = {
  checkoutItems: JSON.parse(localStorage['checkoutItems'] || '[]')
}

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private baseURl = "";
  public Orders;
  constructor(private http: HttpClient,
    private toastrService: ToastrService,
    private router: Router) {
      if(true)
        this.baseURl = environment.apiRoot + '/o/api';
      else      
        this.baseURl = 'https://localhost:7179/Api';
     }
  // get Order History
  public get orders(): Observable<Orders[]> {
    return this.Orders=this.http.get<Orders[]>(this.baseURl+"/Order");
  }

  public cancel(orderId:number): Observable<void> {
    return this.http.delete<void>(this.baseURl+"/Order/"+orderId);
  }

  // Get Checkout Items
  public get saveCart(): Observable<any> {
    const itemsStream = new Observable(observer => {
      observer.next(state.checkoutItems);
      observer.complete();
    });
    return <Observable<any>>itemsStream;
  }

  // Get Checkout Items
  public get checkoutItems(): Observable<any> {
    const itemsStream = new Observable(observer => {
      observer.next(state.checkoutItems);
      observer.complete();
    });
    return <Observable<any>>itemsStream;
  }

  // Create order
  public createOrder(product: any, details: any, orderId: any, amount: any) {
    var item = {
        shippingDetails: details,
        product: product,
        orderId: orderId,
        totalAmount: amount
    };
    state.checkoutItems = item;
    localStorage.setItem("checkoutItems", JSON.stringify(item));
    localStorage.removeItem("cartItems");
    this.router.navigate(['/shop/checkout/success', orderId]);
  }
  
}

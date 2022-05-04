import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';
import { IPayPalConfig, ICreateOrderRequest } from 'ngx-paypal';
import { environment } from '../../../environments/environment';
import { Product } from "../../shared/classes/product";
import { ProductService } from "../../shared/services/product.service";
import { OrderService } from "../../shared/services/order.service";
import { ICheckoutRequest } from 'src/app/shared/classes/Orders';
import { AuthService } from 'src/app/shared/services/auth.service';
import { User } from 'oidc-client';

@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.scss']
})
export class CheckoutComponent implements OnInit {

  public checkoutForm: FormGroup;
  public products: Product[] = [];
  public payPalConfig?: IPayPalConfig;
  public payment: string = 'Stripe';
  public amount: any;
  public user : User;
  constructor(private fb: FormBuilder,
    public productService: ProductService,
    public authService: AuthService,
    private orderService: OrderService) {
    
  }

  ngOnInit(): void {
    this.productService.cartItems.subscribe(response => this.products = response);
    this.getTotal.subscribe(amount => this.amount = amount);
    this.authService.getUserAsync()
    .then(u => {
      this.user = u;
      this.checkoutForm = this.fb.group({
        firstname: [this.user.profile.name, [Validators.required, Validators.pattern('[a-zA-Z][a-zA-Z ]+[a-zA-Z]$')]],
        lastname: [this.user.profile.last_name, [Validators.required, Validators.pattern('[a-zA-Z][a-zA-Z ]+[a-zA-Z]$')]],
        phone: [this.user.profile.phone_number, [Validators.required, Validators.pattern('[0-9]+')]],
        email: [this.user.profile.email, [Validators.required, Validators.email]],
        address: [this.user.profile.address_street, [Validators.required, Validators.maxLength(50)]],
        country: [this.user.profile.address_country, Validators.required],
        town: [this.user.profile.address_city, Validators.required],
        state: [this.user.profile.address_state, Validators.required],
        postalcode: [this.user.profile.address_zip_code, Validators.required]
      });
    });
  }

  public get getTotal(): Observable<number> {
    return this.productService.cartTotalAmount();
  }

  // Stripe Payment Gateway
  stripeCheckout() {
    var request: ICheckoutRequest = {
      userEmail: this.checkoutForm.get('email').value,
      city: this.checkoutForm.get('town').value,
      street: this.checkoutForm.get('address').value,
      aptorunit: "b",
      state: this.checkoutForm.get('state').value,
      zipcode: this.checkoutForm.get('postalcode').value,
      country: this.checkoutForm.get('country').value,
      cardNumber: this.user.profile.card_number,
      cardHolderName: this.user.profile.card_holder,
      cardExpiration: this.user.profile.card_expiration,
      cardSecurityCode: this.user.profile.card_security_number,
    };
    this.productService.placeOrder(request);
  }
}

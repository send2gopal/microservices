    export interface OrderStatus {
        id: number;
        name: string;
    }

    export interface OrderItems {
        productId: number;
        productName: string;
        unitPrice: number;
        quantity: number;
        productImageUrl: string;
    }

    export interface ShippingAddress {
        userId: string;
        street?: any;
        aptOrUnit?: any;
        city?: any;
        state?: any;
        country?: any;
        zipCode?: any;
    }

    export interface PaymentInformation {
        userId: string;
        cardNumber: string;
        cardHolderName: string;
        cardExpiration: Date;
        cardSecurityNumber: string;
        paymentReferenceNumber: string;
        amount: number;
    }

    export interface Orders {
        id: number;
        userId: string;
        orderStatus: OrderStatus;
        enail: string;
        amount: number;
        paymentReferenceNumber: string;
        refundReferenceNumber: string;
        items: OrderItems[];
        shippingAddress: ShippingAddress;
        paymentInformation: PaymentInformation;
    }


export interface Product {
    id: number;
    name: string;
    description: string;
    price: number;
    categoryId: number;
}

export interface Category {
    id: number;
    name: string;
    description: string;
}

export interface Supplier {
    id: number;
    name: string;
    contactInfo: string;
}

export interface Order {
    id: number;
    orderDate: string;
    shippedDate: string;
    customerId: number;
}

export interface OrderDetail {
    id: number;
    orderId: number;
    productId: number;
    quantity: number;
    unitPrice: number;
}

export interface Customer {
    id: number;
    name: string;
    email: string;
    phone: string;
    address: string; 
}

export interface Shipment {
    id: number;
    orderId: number;
    shipmentDate: string;
    trackingNumber: string;
    supplierId: number;
}

export interface StockLevel {
    id: number;
    productId: number;
    warehouseId: number;
    quantity: number;
}

export interface Warehouse {
    id: number;
    location: string;
    manager: string;
}

import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { DataGrid } from 'devextreme-react/data-grid';
import 'devextreme/dist/css/dx.light.css';
import '../ProductCategoryManagement/ProductCategoryManagement.scss';
import { Order, OrderDetail, Customer, Shipment } from '../../models/models';

const OrderManagementPage = () => {
    const [orders, setOrders] = useState<Order[]>([]);
    const [orderDetails, setOrderDetails] = useState<OrderDetail[]>([]);
    const [customers, setCustomers] = useState<Customer[]>([]);
    const [shipments, setShipments] = useState<Shipment[]>([]);

    useEffect(() => {
        fetchData();
    }, []);

    const fetchData = async () => {
        try {
            const ordersResponse = await axios.get('/api/orders');
            const orderDetailsResponse = await axios.get('/api/orderdetails');
            const customersResponse = await axios.get('/api/customers');
            const shipmentsResponse = await axios.get('/api/shipments');
            setOrders(ordersResponse.data);
            setOrderDetails(orderDetailsResponse.data);
            setCustomers(customersResponse.data);
            setShipments(shipmentsResponse.data);
        } catch (error) {
            console.error("There was an error fetching the data!", error);
        }
    };

    const addOrder = async (order: Order) => {
        await axios.post('/api/orders', order);
        fetchData();
    };

    const updateOrder = async (order: Order) => {
        await axios.put(`/api/orders/${order.id}`, order);
        fetchData();
    };

    const deleteOrder = async (id: number) => {
        await axios.delete(`/api/orders/${id}`);
        fetchData();
    };

    const addOrderDetail = async (orderDetail: OrderDetail) => {
        await axios.post('/api/orderdetails', orderDetail);
        fetchData();
    };

    const updateOrderDetail = async (orderDetail: OrderDetail) => {
        await axios.put(`/api/orderdetails/${orderDetail.id}`, orderDetail);
        fetchData();
    };

    const deleteOrderDetail = async (id: number) => {
        await axios.delete(`/api/orderdetails/${id}`);
        fetchData();
    };

    const addCustomer = async (customer: Customer) => {
        await axios.post('/api/customers', customer);
        fetchData();
    };

    const updateCustomer = async (customer: Customer) => {
        await axios.put(`/api/customers/${customer.id}`, customer);
        fetchData();
    };

    const deleteCustomer = async (id: number) => {
        await axios.delete(`/api/customers/${id}`);
        fetchData();
    };

    const addShipment = async (shipment: Shipment) => {
        await axios.post('/api/shipments', shipment);
        fetchData();
    };

    const updateShipment = async (shipment: Shipment) => {
        await axios.put(`/api/shipments/${shipment.id}`, shipment);
        fetchData();
    };

    const deleteShipment = async (id: number) => {
        await axios.delete(`/api/shipments/${id}`);
        fetchData();
    };

    return (
        <div className="order-management-page">
            <h1>Order Management</h1>

            <h2>Orders</h2>
            <DataGrid
                dataSource={orders}
                keyExpr="id"
                showBorders={true}
                onRowInserted={(e) => addOrder(e.data)}
                onRowUpdated={(e) => updateOrder(e.data)}
                onRowRemoved={(e) => deleteOrder(e.data.id)}
            />

            <h2>Order Details</h2>
            <DataGrid
                dataSource={orderDetails}
                keyExpr="id"
                showBorders={true}
                onRowInserted={(e) => addOrderDetail(e.data)}
                onRowUpdated={(e) => updateOrderDetail(e.data)}
                onRowRemoved={(e) => deleteOrderDetail(e.data.id)}
            />

            <h2>Customers</h2>
            <DataGrid
                dataSource={customers}
                keyExpr="id"
                showBorders={true}
                onRowInserted={(e) => addCustomer(e.data)}
                onRowUpdated={(e) => updateCustomer(e.data)}
                onRowRemoved={(e) => deleteCustomer(e.data.id)}
            />

            <h2>Shipments</h2>
            <DataGrid
                dataSource={shipments}
                keyExpr="id"
                showBorders={true}
                onRowInserted={(e) => addShipment(e.data)}
                onRowUpdated={(e) => updateShipment(e.data)}
                onRowRemoved={(e) => deleteShipment(e.data.id)}
            />
        </div>
    );
};

export default OrderManagementPage;

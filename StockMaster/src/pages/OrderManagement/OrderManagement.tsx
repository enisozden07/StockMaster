import React, { useState, useEffect } from 'react';
import api from '../../axiosConfig';
import { DataGrid, Column, Editing, Popup, MasterDetail } from 'devextreme-react/data-grid';
import 'devextreme/dist/css/dx.light.css';
import { Order, OrderDetail, Customer, Product } from '../../models/models';

const OrderManagement = () => {
    const [orders, setOrders] = useState<Order[]>([]);
    const [orderDetails, setOrderDetails] = useState<OrderDetail[]>([]);
    const [customers, setCustomers] = useState<Customer[]>([]);
    const [products, setProducts] = useState<Product[]>([]);

    useEffect(() => {
        fetchOrders();
        fetchCustomers();
        fetchProducts();
        fetchOrderDetails();
    }, []);

    const fetchOrders = async () => {
        try {
            const response = await api.get('/api/orders');
            setOrders(response.data);
        } catch (error) {
            console.error('Error fetching orders:', error);
        }
    };

    const fetchCustomers = async () => {
        try {
            const response = await api.get('/api/customers');
            setCustomers(response.data);
        } catch (error) {
            console.error('Error fetching customers:', error);
        }
    };

    const fetchProducts = async () => {
        try {
            const response = await api.get('/api/products');
            setProducts(response.data);
        } catch (error) {
            console.error('Error fetching products:', error);
        }
    };

    const fetchOrderDetails = async () => {
        try {
            const response = await api.get('/api/orderdetails');
            setOrderDetails(response.data);
        } catch (error) {
            console.error('Error fetching order details:', error);
        }
    };

    const addOrder = async (order: Omit<Order, 'id'>) => {
        try {
            await api.post('/api/orders', order);
            fetchOrders();
        } catch (error) {
            console.error('Error adding order:', error);
        }
    };

    const updateOrder = async (order: Order) => {
        try {
            await api.put(`/api/orders/${order.id}`, order);
            fetchOrders();
        } catch (error) {
            console.error('Error updating order:', error);
        }
    };

    const deleteOrder = async (id: number) => {
        try {
            await api.delete(`/api/orders/${id}`);
            fetchOrders();
        } catch (error) {
            console.error('Error deleting order:', error);
        }
    };

    const addOrderDetail = async (orderDetail: OrderDetail) => {
        try {
            await api.post('/api/orderdetails', orderDetail);
            fetchOrderDetails();
        } catch (error) {
            console.error('Error adding order detail:', error);
        }
    };

    const updateOrderDetail = async (orderDetail: OrderDetail) => {
        try {
            await api.put(`/api/orderdetails/${orderDetail.id}`, orderDetail);
            fetchOrderDetails();
        } catch (error) {
            console.error('Error updating order detail:', error);
        }
    };

    const deleteOrderDetail = async (id: number) => {
        try {
            await api.delete(`/api/orderdetails/${id}`);
            fetchOrderDetails();
        } catch (error) {
            console.error('Error deleting order detail:', error);
        }
    };

    const handleOrderRowInserting = async (e: any) => {
        const { id, ...newData } = e.data;
        await addOrder(newData);
    };

    const handleOrderDetailRowInserting = async (e: any, orderId: number) => {
        const { id, ...newData } = e.data;
        await addOrderDetail({ ...newData, orderId });
    };

    const customerEditorOptions = {
        dataSource: customers,
        displayExpr: (item: Customer) => `${item.id}-${item.name}`,
        valueExpr: 'id',
        searchEnabled: true,
        placeholder: 'Select Customer'
    };

    const productEditorOptions = {
        dataSource: products,
        displayExpr: (item: Product) => `${item.id}-${item.name}`,
        valueExpr: 'id',
        searchEnabled: true,
        placeholder: 'Select Product'
    };

    const OrderDetailGrid = ({ data }: { data: any }) => {
        const orderId = data.key;
        const orderDetailData = orderDetails.filter(od => od.orderId === orderId);

        return (
            <DataGrid
                dataSource={orderDetailData}
                keyExpr="id"
                showBorders={true}
                onRowInserted={async (e) => await handleOrderDetailRowInserting(e, orderId)}
                onRowUpdated={async (e) => await updateOrderDetail(e.data)}
                onRowRemoved={async (e) => await deleteOrderDetail(e.data.id)}
            >
                <Column dataField="id" caption="ID" visible={false} />
                <Column 
                    dataField="productId" 
                    caption="Product" 
                    calculateDisplayValue={(data) => {
                        const product = products.find(p => p.id === data.productId);
                        return product ? `${product.id}-${product.name}` : 'Unknown';
                    }}
                    lookup={productEditorOptions}
                />
                <Column dataField="quantity" caption="Quantity" />
                <Column dataField="unitPrice" caption="Unit Price" />
                <Editing
                    mode="popup"
                    allowAdding={true}
                    allowUpdating={true}
                    allowDeleting={true}
                    useIcons={true}
                >
                    <Popup title="Order Detail Info" showTitle={true} width={700} height={525} />
                </Editing>
            </DataGrid>
        );
    };

    return (
        <div>
            <h1>Order Management</h1>
            <DataGrid
                dataSource={orders}
                keyExpr="id"
                showBorders={true}
                onRowInserted={handleOrderRowInserting}
                onRowUpdated={async (e) => await updateOrder(e.data)}
                onRowRemoved={(e) => deleteOrder(e.data.id)}
            >
                <Column dataField="id" caption="ID" visible={false} />
                <Column 
                    dataField="orderDate" 
                    caption="Order Date" 
                    dataType="date" 
                />
                <Column 
                    dataField="shippedDate" 
                    caption="Shipped Date" 
                    dataType="date" 
                    calculateDisplayValue={(data) => data.shippedDate ? data.shippedDate : 'Not Shipped'}
                />
                <Column 
                    dataField="customerId" 
                    caption="Customer" 
                    calculateDisplayValue={(data) => {
                        const customer = customers.find(c => c.id === data.customerId);
                        return customer ? `${customer.id}-${customer.name}` : 'Unknown';
                    }}
                    lookup={customerEditorOptions}
                />
                <Editing
                    mode="popup"
                    allowAdding={true}
                    allowUpdating={true}
                    allowDeleting={true}
                    useIcons={true}
                >
                    <Popup title="Order Info" showTitle={true} width={700} height={525} />
                </Editing>
                <MasterDetail
                    enabled={true}
                    component={OrderDetailGrid}
                />
            </DataGrid>
        </div>
    );
};

export default OrderManagement;

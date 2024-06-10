import React, { useState, useEffect } from 'react';
import api from '../../axiosConfig';
import { DataGrid, Column, Editing,Pager, Paging, FilterRow, Grouping, GroupPanel, Popup } from 'devextreme-react/data-grid';
import 'devextreme/dist/css/dx.light.css';
import { Shipment, Supplier, Order } from '../../models/models';

const ShipmentManagement = () => {
    const [shipments, setShipments] = useState<Shipment[]>([]);
    const [suppliers, setSuppliers] = useState<Supplier[]>([]);
    const [orders, setOrders] = useState<Order[]>([]);

    useEffect(() => {
        fetchShipments();
        fetchSuppliers();
        fetchOrders();
    }, []);

    const fetchShipments = async () => {
        try {
            const response = await api.get('/api/shipments');
            setShipments(response.data);
        } catch (error) {
            console.error('Error fetching shipments:', error);
        }
    };

    const fetchSuppliers = async () => {
        try {
            const response = await api.get('/api/suppliers');
            setSuppliers(response.data);
        } catch (error) {
            console.error('Error fetching suppliers:', error);
        }
    };

    const fetchOrders = async () => {
        try {
            const response = await api.get('/api/orders');
            setOrders(response.data);
        } catch (error) {
            console.error('Error fetching orders:', error);
        }
    };

    const addShipment = async (shipment: Omit<Shipment, 'id'>) => {
        try {
            await api.post('/api/shipments', shipment);
            fetchShipments();
        } catch (error) {
            console.error('Error adding shipment:', error);
        }
    };

    const updateShipment = async (shipment: Shipment) => {
        try {
            await api.put(`/api/shipments/${shipment.id}`, shipment);
            fetchShipments();
        } catch (error) {
            console.error('Error updating shipment:', error);
        }
    };

    const deleteShipment = async (id: number) => {
        try {
            await api.delete(`/api/shipments/${id}`);
            fetchShipments();
        } catch (error) {
            console.error('Error deleting shipment:', error);
        }
    };

    const handleRowInserting = async (e: any) => {
        const { id, ...newData } = e.data;
        await addShipment(newData);
    };

    const supplierEditorOptions = {
        dataSource: suppliers,
        displayExpr: (item: Supplier) => `${item.id}-${item.name}`,
        valueExpr: 'id',
        searchEnabled: true,
        placeholder: 'Select Supplier'
    };

    const orderEditorOptions = {
        dataSource: orders,
        displayExpr: 'id',
        valueExpr: 'id',
        searchEnabled: true,
        placeholder: 'Select Order'
    };

    return (
        <div>
            <h4>Shipment Management</h4>
            <DataGrid
                dataSource={shipments}
                keyExpr="id"
                showBorders={true}
                onRowInserted={handleRowInserting}
                onRowUpdated={async (e) => await updateShipment(e.data)}
                onRowRemoved={(e) => deleteShipment(e.data.id)}
                columnAutoWidth={true}
                columnHidingEnabled={true}
              >
                <Paging defaultPageSize={10} />
                <Pager showPageSizeSelector={true} showInfo={true} />
                <FilterRow visible={true} />
                <GroupPanel visible={true} />
                <Grouping autoExpandAll={true} />
                <Column dataField="id" caption="ID" visible={false} />
                <Column 
                    dataField="shipmentDate" 
                    caption="Shipment Date" 
                    dataType="date" 
                />
                <Column 
                    dataField="trackingNumber" 
                    caption="Tracking Number" 
                />
                <Column 
                    dataField="supplierId" 
                    caption="Supplier" 
                    calculateDisplayValue={(data) => {
                        const supplier = suppliers.find(s => s.id === data.supplierId);
                        return supplier ? `${supplier.id}-${supplier.name}` : 'Unknown';
                    }}
                    lookup={supplierEditorOptions}
                />
                <Column 
                    dataField="orderId" 
                    caption="Order ID" 
                    lookup={orderEditorOptions}
                />
                <Editing
                    mode="popup"
                    allowAdding={true}
                    allowUpdating={true}
                    allowDeleting={true}
                    useIcons={true}
                >
                    <Popup title="Shipment Info" showTitle={true} width={700} height={525} />
                </Editing>
            </DataGrid>
        </div>
    );
};

export default ShipmentManagement;

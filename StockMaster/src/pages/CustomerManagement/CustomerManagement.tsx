import React, { useState, useEffect } from 'react';
import api from '../../axiosConfig';
import { DataGrid, Column, Editing,Pager, Paging, FilterRow,Popup } from 'devextreme-react/data-grid';
import 'devextreme/dist/css/dx.light.css';
import { Customer } from '../../models/models';

const CustomerManagement = () => {
    const [customers, setCustomers] = useState<Customer[]>([]);

    useEffect(() => {
        fetchCustomers();
    }, []);

    const fetchCustomers = async () => {
        try {
            const response = await api.get('/api/customers');
            setCustomers(response.data);
        } catch (error) {
            console.error('Error fetching customers:', error);
        }
    };

    const addCustomer = async (customer: Omit<Customer, 'id'>) => {
        try {
            await api.post('/api/customers', customer);
            fetchCustomers();
        } catch (error) {
            console.error('Error adding customer:', error);
        }
    };

    const updateCustomer = async (customer: Customer) => {
        try {
            await api.put(`/api/customers/${customer.id}`, customer);
            fetchCustomers();
        } catch (error) {
            console.error('Error updating customer:', error);
        }
    };

    const deleteCustomer = async (id: number) => {
        try {
            await api.delete(`/api/customers/${id}`);
            fetchCustomers();
        } catch (error) {
            console.error('Error deleting customer:', error);
        }
    };

    const handleRowInserting = async (e: any) => {
        const { id, ...newData } = e.data;
        await addCustomer(newData);
    };

    return (
        <div>
            <h1>Customer Management</h1>
            <DataGrid
                dataSource={customers}
                keyExpr="id"
                showBorders={true}
                onRowInserted={handleRowInserting}
                onRowUpdated={async (e) => await updateCustomer(e.data)}
                onRowRemoved={(e) => deleteCustomer(e.data.id)}
                columnAutoWidth={true}
                columnHidingEnabled={true}
              >
                <Paging defaultPageSize={10} />
                <Pager showPageSizeSelector={true} showInfo={true} />
                <FilterRow visible={true} />
                <Column dataField="id" caption="ID" visible={false} />
                <Column dataField="name" caption="Name" />
                <Column dataField="email" caption="Email" />
                <Column dataField="phone" caption="Phone" />
                <Column dataField="address" caption="Address" />
                <Editing
                    mode="popup"
                    allowAdding={true}
                    allowUpdating={true}
                    allowDeleting={true}
                    useIcons={true}
                >
                    <Popup title="Customer Info" showTitle={true} width={700} height={525} />
                </Editing>
            </DataGrid>
        </div>
    );
};

export default CustomerManagement;

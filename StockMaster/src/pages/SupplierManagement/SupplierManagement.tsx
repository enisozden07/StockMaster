import React, { useState, useEffect } from 'react';
import api from '../../axiosConfig';
import { DataGrid, Column, Editing,Pager, Paging, FilterRow, Popup } from 'devextreme-react/data-grid';
import 'devextreme/dist/css/dx.light.css';
import { Supplier } from '../../models/models';

const SupplierManagement = () => {
    const [suppliers, setSuppliers] = useState<Supplier[]>([]);

    useEffect(() => {
        fetchSuppliers();
    }, []);

    const fetchSuppliers = async () => {
        try {
            const response = await api.get('/api/suppliers');
            setSuppliers(response.data);
        } catch (error) {
            console.error('Error fetching suppliers:', error);
        }
    };

    const addSupplier = async (supplier: Omit<Supplier, 'id'>) => {
        try {
            await api.post('/api/suppliers', supplier);
            fetchSuppliers();
        } catch (error) {
            console.error('Error adding supplier:', error);
        }
    };

    const updateSupplier = async (supplier: Supplier) => {
        try {
            await api.put(`/api/suppliers/${supplier.id}`, supplier);
            fetchSuppliers();
        } catch (error) {
            console.error('Error updating supplier:', error);
        }
    };

    const deleteSupplier = async (id: number) => {
        try {
            await api.delete(`/api/suppliers/${id}`);
            fetchSuppliers();
        } catch (error) {
            console.error('Error deleting supplier:', error);
        }
    };

    const handleRowInserting = async (e: any) => {
        const { id, ...newData } = e.data;
        await addSupplier(newData);
    };

    return (
        <div>
            <h1>Supplier Management</h1>
            <DataGrid
                dataSource={suppliers}
                keyExpr="id"
                showBorders={true}
                onRowInserted={handleRowInserting}
                onRowUpdated={async (e) => await updateSupplier(e.data)}
                onRowRemoved={(e) => deleteSupplier(e.data.id)}
                columnAutoWidth={true}
                columnHidingEnabled={true}
              >
                <Paging defaultPageSize={10} />
                <Pager showPageSizeSelector={true} showInfo={true} />
                <FilterRow visible={true} />
                <Column dataField="id" caption="ID" visible={false} />
                <Column 
                    dataField="name" 
                    caption="Name" 
                />
                <Column 
                    dataField="contactInfo" 
                    caption="Contact Info" 
                />
                <Editing
                    mode="popup"
                    allowAdding={true}
                    allowUpdating={true}
                    allowDeleting={true}
                    useIcons={true}
                >
                    <Popup title="Supplier Info" showTitle={true} width={700} height={525} />
                </Editing>
            </DataGrid>
        </div>
    );
};

export default SupplierManagement;

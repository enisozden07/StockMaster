import React, { useState, useEffect } from 'react';
import api from '../../axiosConfig';
import { DataGrid, Column, Editing, Popup,Pager, Paging, FilterRow, MasterDetail } from 'devextreme-react/data-grid';
import 'devextreme/dist/css/dx.light.css';
import { Warehouse, StockLevel, Product } from '../../models/models';

const WarehouseManagement = () => {
    const [warehouses, setWarehouses] = useState<Warehouse[]>([]);
    const [stockLevels, setStockLevels] = useState<StockLevel[]>([]);
    const [products, setProducts] = useState<Product[]>([]);

    useEffect(() => {
        fetchWarehouses();
        fetchStockLevels();
        fetchProducts();
    }, []);

    const fetchWarehouses = async () => {
        try {
            const response = await api.get('/api/warehouses');
            setWarehouses(response.data);
        } catch (error) {
            console.error('Error fetching warehouses:', error);
        }
    };

    const fetchStockLevels = async () => {
        try {
            const response = await api.get('/api/stocklevels');
            setStockLevels(response.data);
        } catch (error) {
            console.error('Error fetching stock levels:', error);
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

    const addWarehouse = async (warehouse: Omit<Warehouse, 'id'>) => {
        try {
            await api.post('/api/warehouses', warehouse);
            fetchWarehouses();
        } catch (error) {
            console.error('Error adding warehouse:', error);
        }
    };

    const updateWarehouse = async (warehouse: Warehouse) => {
        try {
            await api.put(`/api/warehouses/${warehouse.id}`, warehouse);
            fetchWarehouses();
        } catch (error) {
            console.error('Error updating warehouse:', error);
        }
    };

    const deleteWarehouse = async (id: number) => {
        try {
            await api.delete(`/api/warehouses/${id}`);
            fetchWarehouses();
        } catch (error) {
            console.error('Error deleting warehouse:', error);
        }
    };

    const addStockLevel = async (stockLevel: Omit<StockLevel, 'id'>) => {
        try {
            await api.post('/api/stocklevels', stockLevel);
            fetchStockLevels();
        } catch (error) {
            console.error('Error adding stock level:', error);
        }
    };

    const updateStockLevel = async (stockLevel: StockLevel) => {
        try {
            await api.put(`/api/stocklevels/${stockLevel.id}`, stockLevel);
            fetchStockLevels();
        } catch (error) {
            console.error('Error updating stock level:', error);
        }
    };

    const deleteStockLevel = async (id: number) => {
        try {
            await api.delete(`/api/stocklevels/${id}`);
            fetchStockLevels();
        } catch (error) {
            console.error('Error deleting stock level:', error);
        }
    };

    const handleRowInserting = async (e: any) => {
        const { id, ...newData } = e.data;
        await addWarehouse(newData);
    };

    const StockLevelGrid = ({ warehouseId }: { warehouseId: number }) => (
        <DataGrid
            dataSource={stockLevels.filter(sl => sl.warehouseId === warehouseId)}
            keyExpr="id"
            showBorders={true}
            onRowInserted={async (e) => {
                const { id, ...newData } = e.data;
                await addStockLevel({ ...newData, warehouseId });
            }}
            onRowUpdated={async (e) => await updateStockLevel(e.data)}
            onRowRemoved={async (e) => await deleteStockLevel(e.data.id)}
            columnAutoWidth={true}
            columnHidingEnabled={true}
          >
            <Paging defaultPageSize={10} />
            <Pager showPageSizeSelector={true} showInfo={true} />
            <FilterRow visible={true} />
            <Column dataField="id" caption="ID" visible={false} />
            <Column 
                dataField="productId" 
                caption="Product" 
                lookup={{
                    dataSource: products,
                    displayExpr: 'name',
                    valueExpr: 'id'
                }}
            />
            <Column dataField="quantity" caption="Quantity" />
            <Editing
                mode="popup"
                allowAdding={true}
                allowUpdating={true}
                allowDeleting={true}
                useIcons={true}
            >
                <Popup title="Stock Level Info" showTitle={true} width={700} height={525} />
            </Editing>
        </DataGrid>
    );

    return (
        <div>
            <h1>Warehouse Management</h1>
            <DataGrid
                dataSource={warehouses}
                keyExpr="id"
                showBorders={true}
                onRowInserted={handleRowInserting}
                onRowUpdated={async (e) => await updateWarehouse(e.data)}
                onRowRemoved={(e) => deleteWarehouse(e.data.id)}
                columnAutoWidth={true}
                columnHidingEnabled={true}
              >
                <Paging defaultPageSize={10} />
                <Pager showPageSizeSelector={true} showInfo={true} />
                <FilterRow visible={true} />
                <Column dataField="id" caption="ID" visible={false} />
                <Column dataField="location" caption="Location" />
                <Column dataField="manager" caption="Manager" />
                <Editing
                    mode="popup"
                    allowAdding={true}
                    allowUpdating={true}
                    allowDeleting={true}
                    useIcons={true}
                >
                    <Popup title="Warehouse Info" showTitle={true} width={700} height={525} />
                </Editing>
                <MasterDetail
                    enabled={true}
                    component={({ data }) => <StockLevelGrid warehouseId={data.key} />}
                />
            </DataGrid>
        </div>
    );
};

export default WarehouseManagement;

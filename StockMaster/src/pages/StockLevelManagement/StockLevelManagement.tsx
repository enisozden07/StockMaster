import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { DataGrid, Column, Editing } from 'devextreme-react/data-grid';
import 'devextreme/dist/css/dx.light.css';
import './StockLevelManagement.scss';
import { StockLevel, Warehouse, Product } from '../../models/models';

interface CombinedData {
    id: number;
    productId: number;
    warehouseId: number;
    productName: string;
    warehouseLocation: string;
    quantity: number;
}

const StockLevelManagementPage = () => {
    const [data, setData] = useState<CombinedData[]>([]);

    useEffect(() => {
        fetchData();
    }, []);

    const fetchData = async () => {
        try {
            const stockLevelsResponse = await axios.get('/api/stocklevels');
            const warehousesResponse = await axios.get('/api/warehouses');
            const productsResponse = await axios.get('/api/products');
            
            const stockLevels: StockLevel[] = stockLevelsResponse.data;
            const warehouses: Warehouse[] = warehousesResponse.data;
            const products: Product[] = productsResponse.data;

            const combinedData: CombinedData[] = stockLevels.map(stockLevel => {
                const product = products.find(p => p.id === stockLevel.productId);
                const warehouse = warehouses.find(w => w.id === stockLevel.warehouseId);
                return {
                    id: stockLevel.id,
                    productId: stockLevel.productId,
                    warehouseId: stockLevel.warehouseId,
                    productName: product ? product.name : 'Unknown',
                    warehouseLocation: warehouse ? warehouse.location : 'Unknown',
                    quantity: stockLevel.quantity
                };
            });

            setData(combinedData);
        } catch (error) {
            console.error("There was an error fetching the data!", error);
        }
    };

    const addStockLevel = async (data: CombinedData) => {
        const stockLevel: StockLevel = {
            id: data.id,
            productId: data.productId,
            warehouseId: data.warehouseId,
            quantity: data.quantity
        };
        await axios.post('/api/stocklevels', stockLevel);
        fetchData();
    };

    const updateStockLevel = async (data: CombinedData) => {
        const stockLevel: StockLevel = {
            id: data.id,
            productId: data.productId,
            warehouseId: data.warehouseId,
            quantity: data.quantity
        };
        await axios.put(`/api/stocklevels/${stockLevel.id}`, stockLevel);
        fetchData();
    };

    const deleteStockLevel = async (id: number) => {
        await axios.delete(`/api/stocklevels/${id}`);
        fetchData();
    };

    return (
        <div className="stock-level-management-page">
            <h1>Stock Level Management</h1>

            <DataGrid
                dataSource={data}
                keyExpr="id"
                showBorders={true}
                onRowInserted={(e) => addStockLevel(e.data)}
                onRowUpdated={(e) => updateStockLevel(e.data)}
                onRowRemoved={(e) => deleteStockLevel(e.data.id)}
            >
                <Column dataField="id" caption="ID" width={70} />
                <Column dataField="productName" caption="Product Name" />
                <Column dataField="warehouseLocation" caption="Warehouse Location" />
                <Column dataField="quantity" caption="Quantity" />
                <Editing
                    mode="popup"
                    allowAdding={true}
                    allowUpdating={true}
                    allowDeleting={true}
                />
            </DataGrid>
        </div>
    );
};

export default StockLevelManagementPage;

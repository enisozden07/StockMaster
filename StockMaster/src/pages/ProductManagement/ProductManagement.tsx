import React, { useState, useEffect } from 'react';
import api from '../../axiosConfig';
import DataGrid, { Column, Editing, Popup, Paging, Pager } from 'devextreme-react/data-grid';
import 'devextreme/dist/css/dx.light.css';
import { Product, Category } from '../../models/models';

const ProductManagement = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [categories, setCategories] = useState<Category[]>([]);

    useEffect(() => {
        fetchProducts();
        fetchCategories();
    }, []);

    const fetchProducts = async () => {
        try {
            const response = await api.get('/api/products');
            setProducts(response.data);
        } catch (error) {
            console.error('Error fetching products:', error);
        }
    };

    const fetchCategories = async () => {
        try {
            const response = await api.get('/api/categories');
            setCategories(response.data);
        } catch (error) {
            console.error('Error fetching categories:', error);
        }
    };

    const addProduct = async (product: Omit<Product, 'id'>) => {
        try {
            await api.post('/api/products', product);
            fetchProducts();
        } catch (error) {
            console.error('Error adding product:', error);
        }
    };

    const updateProduct = async (product: Product) => {
        try {
            await api.put(`/api/products/${product.id}`, product);
            fetchProducts();
        } catch (error) {
            console.error('Error updating product:', error);
        }
    };

    const deleteProduct = async (id: number) => {
        try {
            await api.delete(`/api/products/${id}`);
            fetchProducts();
        } catch (error) {
            console.error('Error deleting product:', error);
        }
    };

    const handleRowInserting = async (e: any) => {
        const { id, ...newData } = e.data;
        await addProduct(newData);
    };

    const handleRowUpdating = async (e: any) => {
        await updateProduct(e.data);
    };

    const categoryEditorOptions = {
        dataSource: categories,
        displayExpr: 'name',
        valueExpr: 'id',
        searchEnabled: true,
        placeholder: 'Select Category'
    };

    return (
        <div>
            <h4>Product Management</h4>
            <DataGrid
                dataSource={products}
                keyExpr="id"
                showBorders={true}
                onRowInserted={handleRowInserting}
                onRowUpdated={handleRowUpdating}
                onRowRemoved={(e) => deleteProduct(e.data.id)}
            >
                <Column dataField="id" caption="ID" visible={false} />
                <Column dataField="name" caption="Name" />
                <Column dataField="description" caption="Description" />
                <Column dataField="price" caption="Price" />
                <Column 
                    dataField="categoryId" 
                    caption="Category" 
                    calculateDisplayValue={(data) => {
                        const category = categories.find(c => c.id === data.categoryId);
                        return category ? `${category.id} - ${category.name}` : 'Unknown';
                    }}
                    lookup={categoryEditorOptions}
                />
                <Editing
                    mode="popup"
                    allowAdding={true}
                    allowUpdating={true}
                    allowDeleting={true}
                    useIcons={true}
                >
                    <Popup title="Product Info" showTitle={true} width={700} height={525} />
                </Editing>
            </DataGrid>
        </div>
    );
};

export default ProductManagement;

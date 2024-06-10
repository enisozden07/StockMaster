import React, { useState, useEffect } from 'react';
import api from '../../axiosConfig';
import { DataGrid, Column, Editing, Popup,  Pager, Paging, Grouping, GroupPanel } from 'devextreme-react/data-grid';
import { Form, Item, RequiredRule } from 'devextreme-react/form';
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
            console.log('Updating product:', product);
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

    const categoryEditorOptions = {
        dataSource: categories,
        displayExpr: 'name',
        valueExpr: 'id',
        placeholder: 'Select Category'
    };


    return (
        <div>
            <h1>Product Management</h1>
            <DataGrid
                dataSource={products}
                keyExpr="id"
                showBorders={true}
                onRowInserted={(e) => addProduct(e.data)}
                onRowUpdated={(e) => updateProduct(e.data)}
                onRowRemoved={(e) => deleteProduct(e.data.id)}
                columnAutoWidth={true}
                columnHidingEnabled={true}
              >
                <Paging defaultPageSize={10} />
                <Pager showPageSizeSelector={true} showInfo={true} />
                <GroupPanel visible={true} />
                <Grouping autoExpandAll={true} />
                <Column dataField="id" caption="ID" visible={false} />
                <Column dataField="name" caption="Name" />
                <Column dataField="description" caption="Description" />
                <Column dataField="price" caption="Price" />
                <Column
                    dataField="categoryId"
                    caption="Category"
                    lookup={{
                        dataSource: categories,
                        displayExpr: 'name',
                        valueExpr: 'id'
                    }}
                />
                <Editing
                    mode="popup"
                    allowAdding={true}
                    allowUpdating={true}
                    allowDeleting={true}
                    useIcons={true}
                >
                    <Popup title="Product Info" showTitle={true} width={700} height={525} />
                    <Form>
                        <Item dataField="name">
                            <RequiredRule />
                        </Item>
                        <Item dataField="description" />
                        <Item dataField="price" />
                        <Item dataField="categoryId" editorType="dxSelectBox" editorOptions={categoryEditorOptions}>
                            <RequiredRule />
                        </Item>
                    </Form>
                </Editing>
            </DataGrid>
        </div>
    );
};

export default ProductManagement;

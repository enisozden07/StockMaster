import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { DataGrid } from 'devextreme-react/data-grid';
import 'devextreme/dist/css/dx.light.css';
import './ProductCategoryManagement.scss';
import { Product, Category, Supplier } from '../../models/models';

const ProductCategorySupplierManagement = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [categories, setCategories] = useState<Category[]>([]);
    const [suppliers, setSuppliers] = useState<Supplier[]>([]);

    useEffect(() => {
        fetchData();
    }, []);

    const fetchData = async () => {
        try {
            const productsResponse = await axios.get('/api/products');
            const categoriesResponse = await axios.get('/api/categories');
            const suppliersResponse = await axios.get('/api/suppliers');
            setProducts(productsResponse.data);
            setCategories(categoriesResponse.data);
            setSuppliers(suppliersResponse.data);
        } catch (error) {
            console.error("There was an error fetching the data!", error);
        }
    };

    const addProduct = async (product: Product) => {
        await axios.post('/api/products', product);
        fetchData();
    };

    const updateProduct = async (product: Product) => {
        await axios.put(`/api/products/${product.id}`, product);
        fetchData();
    };

    const deleteProduct = async (id: number) => {
        await axios.delete(`/api/products/${id}`);
        fetchData();
    };

    const addCategory = async (category: Category) => {
        await axios.post('/api/categories', category);
        fetchData();
    };

    const updateCategory = async (category: Category) => {
        await axios.put(`/api/categories/${category.id}`, category);
        fetchData();
    };

    const deleteCategory = async (id: number) => {
        await axios.delete(`/api/categories/${id}`);
        fetchData();
    };

    const addSupplier = async (supplier: Supplier) => {
        await axios.post('/api/suppliers', supplier);
        fetchData();
    };

    const updateSupplier = async (supplier: Supplier) => {
        await axios.put(`/api/suppliers/${supplier.id}`, supplier);
        fetchData();
    };

    const deleteSupplier = async (id: number) => {
        await axios.delete(`/api/suppliers/${id}`);
        fetchData();
    };

    return (
        <div className="product-category-supplier-management">
            <h1>Product, Category, and Supplier Management</h1>

            <h2>Products</h2>
            <DataGrid
                dataSource={products}
                keyExpr="id"
                showBorders={true}
                onRowInserted={(e) => addProduct(e.data)}
                onRowUpdated={(e) => updateProduct(e.data)}
                onRowRemoved={(e) => deleteProduct(e.data.id)}
            />

            <h2>Categories</h2>
            <DataGrid
                dataSource={categories}
                keyExpr="id"
                showBorders={true}
                onRowInserted={(e) => addCategory(e.data)}
                onRowUpdated={(e) => updateCategory(e.data)}
                onRowRemoved={(e) => deleteCategory(e.data.id)}
            />

            <h2>Suppliers</h2>
            <DataGrid
                dataSource={suppliers}
                keyExpr="id"
                showBorders={true}
                onRowInserted={(e) => addSupplier(e.data)}
                onRowUpdated={(e) => updateSupplier(e.data)}
                onRowRemoved={(e) => deleteSupplier(e.data.id)}
            />
        </div>
    );
};

export default ProductCategorySupplierManagement;

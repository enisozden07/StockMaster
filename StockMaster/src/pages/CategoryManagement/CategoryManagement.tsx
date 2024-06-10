import React, { useState, useEffect } from 'react';
import api from '../../axiosConfig';
import { DataGrid, Column, Editing, Pager, Paging, FilterRow, Popup } from 'devextreme-react/data-grid';
import 'devextreme/dist/css/dx.light.css';
import { Category } from '../../models/models';

const CategoryManagement = () => {
    const [categories, setCategories] = useState<Category[]>([]);

    useEffect(() => {
        fetchCategories();
    }, []);

    const fetchCategories = async () => {
        try {
            const response = await api.get('/api/categories');
            setCategories(response.data);
        } catch (error) {
            console.error('Error fetching categories:', error);
        }
    };

    const addCategory = async (category: Omit<Category, 'id'>) => {
        try {
            await api.post('/api/categories', category);
            fetchCategories();
        } catch (error) {
            console.error('Error adding category:', error);
        }
    };

    const updateCategory = async (category: Category) => {
        try {
            await api.put(`/api/categories/${category.id}`, category);
            fetchCategories();
        } catch (error) {
            console.error('Error updating category:', error);
        }
    };

    const deleteCategory = async (id: number) => {
        try {
            await api.delete(`/api/categories/${id}`);
            fetchCategories();
        } catch (error) {
            console.error('Error deleting category:', error);
        }
    };

    const handleRowInserting = async (e: any) => {
        const { id, ...newData } = e.data;
        await addCategory(newData);
    };

    const handleRowUpdating = async (e: any) => {
        await updateCategory(e.data);
    };

    return (
        <div>
            <h1>Category Management</h1>
            <DataGrid
                dataSource={categories}
                keyExpr="id"
                showBorders={true}
                onRowInserted={handleRowInserting}
                onRowUpdated={handleRowUpdating}
                onRowRemoved={(e) => deleteCategory(e.data.id)}
                columnAutoWidth={true}
                columnHidingEnabled={true}
              >
                <Paging defaultPageSize={10} />
                <Pager showPageSizeSelector={true} showInfo={true} />
                <FilterRow visible={true} />
                <Column dataField="id" caption="ID" visible={false} />
                <Column dataField="name" caption="Name" />
                <Column dataField="description" caption="Description" />
                <Editing
                    mode="popup"
                    allowAdding={true}
                    allowUpdating={true}
                    allowDeleting={true}
                    useIcons={true}
                >
                    <Popup title="Category Info" showTitle={true} width={700} height={525} />
                </Editing>
            </DataGrid>
        </div>
    );
};

export default CategoryManagement;

import React, { useState, useEffect } from 'react';
import api from '../../axiosConfig';
import DataGrid, { Column, Paging, Pager } from 'devextreme-react/data-grid';
import Chart, { ArgumentAxis, ValueAxis, Series, Label, Legend, Export, Title, Tooltip } from 'devextreme-react/chart';
import PieChart, { Series as PieSeries, Label as PieLabel, Connector, Size } from 'devextreme-react/pie-chart';
import 'devextreme/dist/css/dx.light.css';
import './dashboard.scss'; 

const Dashboard = () => {
  const [metrics, setMetrics] = useState({
    totalProducts: 0,
    totalOrders: 0,
    totalCustomers: 0,
    totalShipments: 0,
  });

  const [recentOrders, setRecentOrders] = useState([]);
  const [recentShipments, setRecentShipments] = useState([]);
  const [productDistribution, setProductDistribution] = useState([]);
  const [salesFunnel, setSalesFunnel] = useState([]);
  const [salesOverview, setSalesOverview] = useState([]);

  useEffect(() => {
    fetchMetrics();
    fetchRecentOrders();
    fetchRecentShipments();
    fetchProductDistribution();
    fetchSalesOverview();
  }, []);

  const fetchMetrics = async () => {
    try {
      const response = await api.get('/api/dashboard/metrics');
      setMetrics(response.data);
    } catch (error) {
      console.error('Error fetching metrics:', error);
    }
  };

  const fetchRecentOrders = async () => {
    try {
      const response = await api.get('/api/dashboard/recent-orders');
      setRecentOrders(response.data);
    } catch (error) {
      console.error('Error fetching recent orders:', error);
    }
  };

  const fetchRecentShipments = async () => {
    try {
      const response = await api.get('/api/dashboard/recent-shipments');
      setRecentShipments(response.data);
    } catch (error) {
      console.error('Error fetching recent shipments:', error);
    }
  };

  const fetchProductDistribution = async () => {
    try {
      const response = await api.get('/api/dashboard/product-distribution');
      setProductDistribution(response.data);
    } catch (error) {
      console.error('Error fetching product distribution:', error);
    }
  };

  const fetchSalesOverview = async () => {
    try {
      const response = await api.get('/api/dashboard/sales-overview');
      setSalesOverview(response.data);
    } catch (error) {
      console.error('Error fetching sales overview:', error);
    }
  };

  return (
    <div className="dashboard">
      <div className="dashboard-metrics">
        <div className="metric-card">
          <h5>Total Products</h5>
          <p>{metrics.totalProducts}</p>
        </div>
        <div className="metric-card">
          <h5>Total Orders</h5>
          <p>{metrics.totalOrders}</p>
        </div>
        <div className="metric-card">
          <h5>Total Customers</h5>
          <p>{metrics.totalCustomers}</p>
        </div>
        <div className="metric-card">
          <h5>Total Shipments</h5>
          <p>{metrics.totalShipments}</p>
        </div>
      </div>

      <div className="dashboard-charts">
        <div id="chart" className="chart-container">
          <Chart dataSource={salesOverview} title="Sales Overview">
            <ArgumentAxis>
              <Label rotationAngle={45} overlappingBehavior="rotate" />
            </ArgumentAxis>
            <ValueAxis>
              <Title text="Number of Orders" />
            </ValueAxis>
            <Series
              type="bar"
              argumentField="orderDate"
              valueField="orderCount"
            />
            <Tooltip enabled={true} />
            <Legend visible={false} />
            <Export enabled={true} />
          </Chart>
        </div>

        <div id="pie" className="pie-container">
          <PieChart dataSource={productDistribution} type="doughnut">
            <PieSeries
              argumentField="productName"
              valueField="quantity"
            >
              <PieLabel visible={true} position="columns" customizeText={(pointInfo) => `${pointInfo.argument}: ${pointInfo.value}`}>
                <Connector visible={true} />
              </PieLabel>
            </PieSeries>
            <Size width={500} />
          </PieChart>
        </div>
      </div>

      <h2>Recent Orders</h2>
      <DataGrid
        dataSource={recentOrders}
        showBorders={true}
        columnAutoWidth={true}
        className="custom-datagrid"
      >
        <Column dataField="id" caption="Order ID" />
        <Column dataField="orderDate" caption="Order Date" dataType="date" format="yyyy-MM-dd" />
        <Column dataField="customerName" caption="Customer" />
        <Paging defaultPageSize={5} />
        <Pager showPageSizeSelector={true} allowedPageSizes={[5, 10, 20]} showInfo={true} />
      </DataGrid>

      <h2>Recent Shipments</h2>
      <DataGrid
        dataSource={recentShipments}
        showBorders={true}
        columnAutoWidth={true}
        className="custom-datagrid"
      >
        <Column dataField="id" caption="Shipment ID" />
        <Column dataField="shipmentDate" caption="Shipment Date" dataType="date" format="yyyy-MM-dd" />
        <Column dataField="trackingNumber" caption="Tracking Number" />
        <Paging defaultPageSize={5} />
        <Pager showPageSizeSelector={true} allowedPageSizes={[5, 10, 20]} showInfo={true} />
      </DataGrid>
    </div>
  );
};

export default Dashboard;

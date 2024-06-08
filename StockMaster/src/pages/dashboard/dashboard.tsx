import React, { useEffect, useState } from 'react';
import axios from 'axios';
import DataGrid, { Column } from 'devextreme-react/data-grid';
import Chart, {
  ArgumentAxis,
  ValueAxis,
  Series,
  Label,
  Legend,
  Export,
  Title,
  Tooltip
} from 'devextreme-react/chart';
import PieChart, {
  Series as PieSeries,
  Label as PieLabel,
  Connector,
  Size
} from 'devextreme-react/pie-chart';
import Funnel, {
  Label as FunnelLabel,
  Tooltip as FunnelTooltip
} from 'devextreme-react/funnel';
import './dashboard.scss';

const DashboardPage = () => {
  const [dashboardData, setDashboardData] = useState<any>(null);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchDashboardData = async () => {
      try {
        const response = await axios.get('https://localhost:7100/api/dashboard');
        console.log('Dashboard Data:', response.data); // Log data to verify
        setDashboardData(response.data);
      } catch (error) {
        console.error('Error fetching dashboard data:', error);
        setError('Error fetching dashboard data');
      } finally {
        setLoading(false);
      }
    };

    fetchDashboardData();
  }, []);

  if (loading) {
    return <div>Loading...</div>;
  }

  if (error) {
    return <div>{error}</div>;
  }

  if (!dashboardData) {
    return <div>No data available</div>;
  }

  const {
    Opportunities,
    RevenueTotal,
    Conversion,
    Leads,
    RevenueAnalysis,
    ConversionFunnel,
    RevenueSnapshot
  } = dashboardData;

  return (
    <div className="dashboard">
      <h4>Dashboard</h4>
      <div className="dashboard-metrics">
        <div className="metric-card">
          <h5>Opportunities</h5>
          <p>${Opportunities}</p>
        </div>
        <div className="metric-card">
          <h5>Revenue Total</h5>
          <p>${RevenueTotal}</p>
        </div>
        <div className="metric-card">
          <h5>Conversion</h5>
          <p>{Conversion}%</p>
        </div>
        <div className="metric-card">
          <h5>Leads</h5>
          <p>{Leads}</p>
        </div>
      </div>
      <div className="dashboard-charts">
        <div className="dashboard-cards">
          <Chart
            title="Revenue Analysis"
            dataSource={RevenueAnalysis}
            id="chart"
          >
            <ArgumentAxis>
              <Label />
            </ArgumentAxis>
            <ValueAxis>
              <Label />
            </ValueAxis>
            <Series
              valueField="revenue"
              argumentField="month"
              type="line"
              color="#ffaa66"
            />
            <Legend verticalAlignment="bottom" horizontalAlignment="center" />
            <Export enabled={true} />
            <Title text="Revenue Analysis" />
            <Tooltip enabled={true} />
          </Chart>
        </div>
        <div className="dashboard-cards">
          <Funnel
            id="funnel"
            title="Conversion Funnel"
            dataSource={ConversionFunnel}
            argumentField="stage"
            valueField="value"
          >
            <FunnelTooltip enabled={true} />
            <FunnelLabel visible={true} />
          </Funnel>
        </div>
      </div>
      <div className="dashboard-grids">
        <div className="dashboard-cards">
          <PieChart
            id="pie"
            dataSource={RevenueSnapshot}
            title="Revenue Snapshot (All Products)"
          >
            <PieSeries argumentField="category" valueField="value">
              <PieLabel visible={true}>
                <Connector visible={true} />
              </PieLabel>
            </PieSeries>
            <Size height={400} />
          </PieChart>
        </div>
        <div className="dashboard-cards">
          <DataGrid dataSource={RevenueAnalysis} keyExpr="month">
            <Column dataField="month" caption="Month" />
            <Column dataField="revenue" caption="Revenue" />
          </DataGrid>
        </div>
      </div>
    </div>
  );
};

export default DashboardPage;

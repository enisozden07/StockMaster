import { ProfilePage, DashboardPage,  ProductManagementPage, CategoryManagementPage, OrderManagementPage, CustomerManagementPage, ShipmentManagementPage, SupplierManagementPage, WarehouseManagementPage } from './pages';
import { withNavigationWatcher } from './contexts/navigation';

const routes = [
  {
    path: '/',
    element: DashboardPage
  },
  {
      path: '/profile',
      element: ProfilePage
  },
  {
    path: '/dashboard',
    element: DashboardPage
  },
  {
    path: '/product-management',
    element: ProductManagementPage
  }, 
  {
    path: '/category-management',
    element: CategoryManagementPage
  }, 
  {
    path: '/order-management',
    element: OrderManagementPage
  }, 
  {
    path: '/customer-management',
    element: CustomerManagementPage
  }, 
  {
    path: '/shipment-management',
    element: ShipmentManagementPage
  }, 
  {
    path: '/supplier-management',
    element: SupplierManagementPage
  }, 
  {
    path: '/warehouse-management',
    element: WarehouseManagementPage
  }
];

export default routes.map(route => {
    return {
        ...route,
        element: withNavigationWatcher(route.element, route.path)
    };
});

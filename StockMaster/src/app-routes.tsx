import { HomePage, TasksPage, ProfilePage, DashboardPage, ProductCategoryManagementPage, OrderManagementPagePage, StockLevelManagementPage } from './pages';
import { withNavigationWatcher } from './contexts/navigation';

const routes = [
    {
        path: '/tasks',
        element: TasksPage
    },
    {
        path: '/profile',
        element: ProfilePage
    },
    {
        path: '/home',
        element: HomePage
    }, 
  {
    path: '/dashboard',
    element: DashboardPage
  }, 
  {
    path: '/product-category-management',
    element: ProductCategoryManagementPage
  }, 
  {
    path: '/order-management-page',
    element: OrderManagementPagePage
  }, 
  {
    path: '/stock-level-management',
    element: StockLevelManagementPage
  }
];

export default routes.map(route => {
    return {
        ...route,
        element: withNavigationWatcher(route.element, route.path)
    };
});

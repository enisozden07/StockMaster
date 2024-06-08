import React from 'react';
import { HashRouter as Router } from 'react-router-dom';
import LoadPanel from 'devextreme-react/load-panel';
import { NavigationProvider } from './contexts/navigation';
import { AuthProvider, useAuth } from './contexts/auth';
import { ProductProvider } from './contexts/ProductContext';
import { useScreenSizeClass } from './utils/media-query';
import Content from './Content';
import UnauthenticatedContent from './UnauthenticatedContent';
import 'devextreme/dist/css/dx.common.css';
import './themes/generated/theme.base.css';
import './themes/generated/theme.additional.css';
import './dx-styles.scss';

import {licenseKey} from './devextreme-license'
import config from 'devextreme/core/config'
config({
    licenseKey
})

function App() {
  const { user, loading } = useAuth();

  if (loading) {
    return <LoadPanel visible={true} />;
  }

  if (user) {
    return <Content />;
  }

  return <UnauthenticatedContent />;
}

export default function Root() {
  const screenSizeClass = useScreenSizeClass();

  return (
    <Router>
      <AuthProvider>
        <NavigationProvider>
          <ProductProvider>
            <div className={`app ${screenSizeClass}`}>
              <App />
            </div>
          </ProductProvider>
        </NavigationProvider>
      </AuthProvider>
    </Router>
  );
}
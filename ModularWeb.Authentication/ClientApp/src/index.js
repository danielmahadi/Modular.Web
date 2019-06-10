import React from 'react';
import ReactDOM from 'react-dom';

import App from './App';

const rootElement = document.getElementById("root");

function renderApp() {
  ReactDOM.render(
    <App />,
    rootElement
  );  
}


if (module.hot) {
  module.hot.accept("./App", () => {
    renderApp(App);
  });
}

renderApp();

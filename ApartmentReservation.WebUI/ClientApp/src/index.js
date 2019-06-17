import "bootstrap/dist/css/bootstrap.css";
import "react-day-picker/lib/style.css";
import "./index.css";
import React from "react";
import ReactDOM from "react-dom";
import App from "./App";
import registerServiceWorker from "./registerServiceWorker";
import { createStore, applyMiddleware } from "redux";
import rootReducer from "./store/reducers/rootReducer";
import { Provider } from "react-redux";
import thunk from "redux-thunk";
import { createLogger } from "redux-logger";

const rootElement = document.getElementById("root");

const logger = createLogger();
const store = createStore(rootReducer, applyMiddleware(thunk, logger));

const app = () => {
  return (
    <Provider store={store}>
      <App />
    </Provider>
  );
};

ReactDOM.render(app(), rootElement);

registerServiceWorker();

import React, { Component } from "react";
import { Route } from "react-router";
import { Layout } from "./components/Layout";
import { Home } from "./components/Home";
import Login from "./components/Account/Login";
import Logout from "./components/Account/Logout";
import Register from "./components/Account/Register";

export default class App extends Component {
  displayName = App.name;

  render() {
    return (
      <Layout>
        <Route exact path="/" component={Home} />
        <Route exact path="/Account/Login" component={Login} />
        <Route exact path="/Account/Logout" component={Logout} />
        <Route exact path="/Account/Register" component={Register} />
      </Layout>
    );
  }
}

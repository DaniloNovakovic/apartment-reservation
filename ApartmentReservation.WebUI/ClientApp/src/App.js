import React, { Component } from "react";
import { Route } from "react-router";
import { Router } from "react-router-dom";
import { connect } from "react-redux";

import { PrivateRoute } from "./components/PrivateRoute";
import { Layout } from "./components/layout/Layout";
import { Home } from "./components/Home";
import Login from "./components/auth/Login";
import Logout from "./components/auth/Logout";
import Register from "./components/auth/Register";
import Users from "./components/users/Users";
import Profile from "./components/account/Profile";
import { history } from "./helpers";
import { alertActions } from "./store/actions";

class App extends Component {
  constructor(props) {
    super(props);

    history.listen((location, action) => {
      // clear alert on location change
      this.props.clear();
    });
  }

  render() {
    return (
      <Router history={history}>
        <Layout>
          <Route exact path="/" component={Home} />
          <Route exact path="/Account/Login" component={Login} />
          <Route exact path="/Account/Logout" component={Logout} />
          <Route exact path="/Account/Register" component={Register} />
          <PrivateRoute exact path="/Profile" component={Profile} />
          <PrivateRoute exact path="/Users" component={Users} />
        </Layout>
      </Router>
    );
  }
}

function mapStateToProps(state) {
  const { alert } = state;
  return {
    alert
  };
}

export default connect(
  mapStateToProps,
  { ...alertActions }
)(App);

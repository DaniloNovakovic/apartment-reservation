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
import { roleNames } from "./constants";
import AddHost from "./components/users/AddHost";
import AddGuest from "./components/users/AddGuest";
import EditUser from "./components/users/EditUser";
import AddApartment from "./components/apartments/AddApartment";
import ViewApartment from "./components/apartments/info/ViewApartment";
import Amenities from "./components/amenities/Amenities";
import EditAmenity from "./components/amenities/EditAmenity";
import AddAmenity from "./components/amenities/AddAmenity";
import Reservations from "./components/reservations/Reservations";

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
          <PrivateRoute
            exact
            path="/Users"
            roles={[roleNames.Admin]}
            component={Users}
          />
          <PrivateRoute
            path="/add-guest"
            roles={[roleNames.Admin]}
            component={AddGuest}
          />
          <PrivateRoute
            path="/add-host"
            roles={[roleNames.Admin]}
            component={AddHost}
          />
          <PrivateRoute
            path="/edit-user/:id"
            roles={[roleNames.Admin]}
            component={EditUser}
          />
          <PrivateRoute
            path="/add-apartment"
            roles={[roleNames.Host]}
            component={AddApartment}
          />
          <Route path="/view-apartment/:id" component={ViewApartment} />
          <PrivateRoute
            path="/amenities"
            roles={[roleNames.Admin]}
            component={Amenities}
          />
          <PrivateRoute
            path="/add-amenity"
            roles={[roleNames.Admin]}
            component={AddAmenity}
          />
          <PrivateRoute
            path="/edit-amenity/:id"
            roles={[roleNames.Admin]}
            component={EditAmenity}
          />
          <PrivateRoute path="/reservations" component={Reservations} />
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

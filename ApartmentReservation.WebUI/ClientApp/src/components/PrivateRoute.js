import React from "react";
import { Route, Redirect } from "react-router-dom";

export const PrivateRoute = ({ component: Component, roles, ...rest }) => (
  <Route
    {...rest}
    render={props => {
      let currentUser = localStorage.getItem("user");
      if (!currentUser) {
        // not logged in so redirect to login page with the return url
        return (
          <Redirect
            to={{ pathname: "/Account/Login", state: { from: props.location } }}
          />
        );
      }

      currentUser = JSON.parse(currentUser);

      // check if route is restricted by role
      if (roles && roles.indexOf(currentUser.roleName) === -1) {
        console.log(roles.indexOf(currentUser.roleName));
        // role not authorised so redirect to home page
        return <Redirect to={{ pathname: "/" }} />;
      }

      // authorised so return component
      return <Component {...props} />;
    }}
  />
);

import React, { Component } from "react";
import { userService } from "../../services";
import { EditProfile } from "../account/EditProfile";
import { history } from "../../helpers";
import { Spinner } from "react-bootstrap";

export default class EditUser extends Component {
  constructor(props) {
    super(props);
    this.state = {
      loading: true,
      userId: props.match.params.id,
      user: {}
    };
    userService.getById(this.state.userId).then(user => {
      this.setState({ loading: false, user });
    });
  }

  handleSubmit = user => {
    userService.update(user).then(_ => {
      history.push("/Users");
    });
  };

  render() {
    return (
      <div>
        {this.state.loading ? (
          <Spinner animation="grow" variant="secondary" role="status">
            <span className="sr-only">Loading...</span>
          </Spinner>
        ) : (
          <EditProfile
            user={this.state.user}
            handleSubmit={this.handleSubmit}
          />
        )}
      </div>
    );
  }
}

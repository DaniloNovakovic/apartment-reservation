import React, { Component } from "react";
import { connect } from "react-redux";
import { ViewProfile } from "./ViewProfile";
import { EditProfile } from "./EditProfile";
import { updateCurrentUser, alertActions } from "../../store/actions";
import { Spinner, Alert } from "react-bootstrap";

export class Profile extends Component {
  constructor(props) {
    super(props);
    this.state = {
      edit: false,
      loading: false
    };
  }

  toggleEdit = () => {
    this.props.clearAlert();
    this.setState({
      edit: !this.state.edit
    });
  };

  handleSubmit = user => {
    this.setState({ loading: true });
    this.props.updateCurrentUser(user).then(_ => {
      this.setState({
        loading: false,
        edit: alert.type === "success"
      });
    });
  };

  render() {
    const { alert, user } = this.props;
    const { edit, loading } = this.state;
    let profileContent = edit ? (
      <EditProfile
        user={user}
        handleSubmit={this.handleSubmit}
        handleCancel={this.toggleEdit}
      />
    ) : (
      <ViewProfile user={user} handleEditClick={this.toggleEdit} />
    );

    return (
      <section>
        <h1>Profile</h1>
        {loading ? (
          <Spinner animation="grow" variant="primary" role="status">
            <span className="sr-only">Loading...</span>
          </Spinner>
        ) : (
          <main>
            {alert && alert.message ? (
              <Alert variant={alert.type}>{alert.message}</Alert>
            ) : null}
            {profileContent}
          </main>
        )}
      </section>
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.auth.user,
    alert: state.alert
  };
};

export default connect(
  mapStateToProps,
  { updateCurrentUser, clearAlert: alertActions.clear }
)(Profile);

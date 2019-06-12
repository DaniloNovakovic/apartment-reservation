import React, { Component } from "react";
import { connect } from "react-redux";
import { ViewProfile } from "./ViewProfile";
import { EditProfile } from "./EditProfile";
import { userService } from "../../services";

export class Profile extends Component {
  constructor(props) {
    super(props);
    this.state = { edit: false, user: props.user, loading: false };
  }

  toggleEdit = () => {
    this.setState({
      edit: !this.state.edit
    });
  };

  handleSubmit = user => {
    this.toggleEdit();
    this.setState({ loading: true });
    userService.updateCurrentUser(user).then(_ => {
      this.setState({ user, loading: false });
    });
  };

  render() {
    const { user, edit, loading } = this.state;
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
      <div>
        <h1>Profile</h1>
        {loading ? (
          <p>
            <em>Loading...</em>
          </p>
        ) : (
          profileContent
        )}
      </div>
    );
  }
}

const mapStateToProps = state => {
  return {
    user: state.auth.user
  };
};

export default connect(mapStateToProps)(Profile);

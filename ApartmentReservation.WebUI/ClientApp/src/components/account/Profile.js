import React, { Component } from "react";
import { connect } from "react-redux";
import { ViewProfile } from "./ViewProfile";
import { EditProfile } from "./EditProfile";

export class Profile extends Component {
  constructor(props) {
    super(props);
    this.state = { edit: false, user: props.user };
  }

  toggleEdit = () => {
    this.setState({
      edit: !this.state.edit
    });
  };

  handleSubmit = user => {
    // TODO: Update user
    this.toggleEdit();
  };

  render() {
    const { user, edit } = this.state;

    return (
      <div>
        <h1>Profile</h1>
        {edit ? (
          <EditProfile
            user={user}
            handleSubmit={this.handleSubmit}
            handleCancel={this.toggleEdit}
          />
        ) : (
          <ViewProfile user={user} handleEditClick={this.toggleEdit} />
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

import React, { Component } from "react";
import { connect } from "react-redux";
import { Form, FormGroup, Button, Alert, Card } from "react-bootstrap";
import ReactStars from "react-stars";
import { commentService } from "../../../../services";

export class PostComment extends Component {
  state = {
    rating: 5,
    text: "",
    alert: null
  };
  ratingChanged = newRating => {
    this.setState({ ...this.state, rating: newRating });
  };
  textChanged = event => {
    this.setState({ ...this.state, text: event.target.value });
  };
  handleSubmit = event => {
    event.preventDefault();
    const commentData = {
      rating: this.state.rating,
      text: this.state.text,
      guestId: this.props.userId,
      apartmentId: this.props.apartmentId
    };
    commentService.post(commentData).then(
      _ => {
        this.setState({
          alert: {
            type: "success",
            text: "Your comment will be visible after approval"
          }
        });
      },
      err => {
        this.setState({
          alert: {
            type: "danger",
            text: err
          }
        });
      }
    );
  };
  render() {
    const { alert } = this.state;

    return alert && alert.type ? (
      <Alert variant={alert.type}>{alert.text}</Alert>
    ) : (
      <Card>
        <Card.Body>
          <Card.Title>Add an item</Card.Title>
          <Form onSubmit={this.handleSubmit}>
            <FormGroup>
              <Form.Label>Rating</Form.Label>
              <ReactStars
                count={5}
                value={this.state.rating}
                half={false}
                onChange={this.ratingChanged}
              />
            </FormGroup>
            <FormGroup>
              <Form.Label>Review</Form.Label>
              <Form.Control
                as="textarea"
                name="text"
                value={this.state.text}
                onChange={this.textChanged}
                rows={2}
                placeholder="Enter your review here..."
                required
              />
            </FormGroup>
            <Button type="submit" variant="primary">
              Submit
            </Button>
          </Form>
        </Card.Body>
      </Card>
    );
  }
}
const mapStateToProps = ({ auth, apartment }) => {
  const { id: userId } = auth.user || {};
  const { id: apartmentId } = apartment.currentApartment || {};
  return {
    userId,
    apartmentId
  };
};

export default connect(mapStateToProps)(PostComment);

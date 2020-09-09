import React from "react";
import { commentService } from "../../../services";
import { makeCancelable } from "../../../helpers";
import PostComment from "./edit/PostComment";
import { Button, Media } from "react-bootstrap";
import ReactStars from "react-stars";

export class ViewApartmentComments extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      comments: props.comments,
    };
  }
  approve = (index) => {
    const { comments = [] } = this.state;
    if (index < 0 || index >= comments.length) return;

    let currComment = comments[index];
    this.promise = makeCancelable(commentService.approve(currComment.id));
    this.promise.then((_) => {
      let newComments = [...comments];
      newComments[index] = { ...currComment, approved: true };
      this.setState({ ...this.state, comments: newComments });
    });
  };
  render() {
    const comments = this.state.comments || [];
    const canPostComments = this.props.canPostComments || false;
    const canApprove = this.props.canApprove || false;

    return (
      <article className="view-comments">
        <h5>Comments</h5>
        {comments.length === 0 ? (
          <p>No comments available</p>
        ) : (
        <ul className="list-unstyled">
            {comments.filter(item => item.approved).map((item, index) => {
              return (
                <Media as="li" key={`comment-${item.id}`}>
                  <Media.Body className="review">
                    {index !== 0 && <hr />}
                    <h5 className="username">{item.guestUsername}</h5>
                    <ReactStars
                      className="rating"
                      count={5}
                      value={item.rating}
                      edit={false}
                    />
                    <p className="text">{item.text}</p>
                    {!item.approved && canApprove && (
                      <Button
                        variant="success"
                        onClick={() => this.approve(index)}
                      >
                        Approve
                      </Button>
                    )}
                  </Media.Body>
                </Media>
              );
            })}
          </ul>
        )}
        {canPostComments && <PostComment />}
      </article>
    );
  }
}

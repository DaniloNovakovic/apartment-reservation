import React from "react";
import { commentService } from "../../../services";
import { makeCancelable } from "../../../helpers";
import PostComment from "./edit/PostComment";
import { Button } from "react-bootstrap";

export class ViewApartmentComments extends React.Component {
  constructor(props) {
    super(props);
    this.state = {
      comments: []
    };
    this.promise = null;
  }
  componentDidMount() {
    const apartmentId = this.props.apartment && this.props.apartment.id;
    this.promise = makeCancelable(commentService.getAll({ apartmentId }));
    this.promise
      .then(comments => {
        this.setState({ ...this.state, comments });
      })
      .catch(_ => {});
  }
  componentWillUnmount() {
    if (this.promise) {
      this.promise.cancel();
    }
  }
  approve = index => {
    const { comments = [] } = this.state;
    if (index < 0 || index >= comments.length) return;

    let currComment = comments[index];
    this.promise = makeCancelable(commentService.approve(currComment.id));
    this.promise.then(_ => {
      let newComments = [...comments];
      newComments[index] = { ...currComment, approved: true };
      this.setState({ ...this.state, comments: newComments });
    });
  };
  render() {
    const comments = this.state.comments || [];
    const canPostComments = this.props.canPostComments || false;

    return (
      <article className="view-comments">
        <h5>Comments</h5>
        {comments.length === 0 ? (
          <p>No comments available</p>
        ) : (
          <ul>
            {comments.map((item, index) => {
              return (
                <li key={`comment-${index}`}>
                  <div>
                    <pre>{JSON.stringify(item, null, 2)}</pre>
                    {!item.approved && (
                      <Button onClick={() => this.approve(index)}>
                        Approve
                      </Button>
                    )}
                  </div>
                </li>
              );
            })}
          </ul>
        )}
        {canPostComments && <PostComment />}
      </article>
    );
  }
}

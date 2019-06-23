import React from "react";
import { commentService } from "../../../services";
import { makeCancelable } from "../../../helpers";
import PostComment from "./edit/PostComment";

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
        console.log(comments);
        this.setState({ ...this.state, comments });
      })
      .catch(_ => {});
  }
  componentWillUnmount() {
    if (this.promise) {
      this.promise.cancel();
    }
  }
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
              return <li key={`comment-${index}`}>{JSON.stringify(item)}</li>;
            })}
          </ul>
        )}
        {canPostComments && <PostComment />}
      </article>
    );
  }
}

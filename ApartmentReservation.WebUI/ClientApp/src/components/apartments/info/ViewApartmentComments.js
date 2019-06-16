import React from "react";

export const ViewApartmentComments = ({ comments = [], allowEdit = false }) => (
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
  </article>
);

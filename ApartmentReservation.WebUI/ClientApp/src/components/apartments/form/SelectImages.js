import React, { Component } from "react";
import Gallery from "react-photo-gallery";
import SelectedImage from "./SelectedImage";
import { Button } from "react-bootstrap";

const mapImages = images => {
  if (!images) {
    return [];
  }

  return images.map(img => {
    return {
      src: img.uri,
      id: img.id,
      width: 3,
      height: 2
    };
  });
};

export default class SelectImages extends Component {
  constructor(props) {
    super(props);
    this.state = {
      images: mapImages(this.props.images),
      selectAll: false
    };
  }
  selectPhoto = (event, obj) => {
    let currImages = [...this.state.images];
    currImages[obj.index].selected = !currImages[obj.index].selected;
    this.setState({
      images: currImages
    });
    this.notifyParent(currImages);
  };
  toggleSelect = () => {
    let currImages = [...this.state.images].map(img => {
      return { ...img, selected: !this.state.selectAll };
    });
    this.setState({
      images: currImages,
      selectAll: !this.state.selectAll
    });
    this.notifyParent(currImages);
  };

  notifyParent = images => {
    if (this.props.handleChange) {
      const selectedImages = images.filter(img => !!img.selected);
      this.props.handleChange({
        target: {
          name: "selectedImages",
          value: selectedImages
        }
      });
    }
  };

  render() {
    return (
      <div>
        <p>
          <Button
            variant="primary"
            className="toggle-select"
            onClick={this.toggleSelect}
          >
            toggle select all
          </Button>
        </p>
        <Gallery
          photos={this.state.images}
          onClick={this.selectPhoto}
          renderImage={SelectedImage}
          direction={"column"}
        />
      </div>
    );
  }
}

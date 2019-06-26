import "ol/ol.css";
import "./OpenLayersMap.css";
import React, { Component } from "react";
import Map from "ol/Map.js";
import View from "ol/View.js";
import TileLayer from "ol/layer/Tile.js";
import OSM from "ol/source/OSM.js";
import { fromLonLat } from "ol/proj";

export class OpenLayersMap extends Component {
  constructor(props) {
    super(props);
    this.mapRef = React.createRef();
  }
  componentDidMount() {
    const { lon = 0, lat = 0 } = this.props;

    var map = new Map({
      layers: [
        new TileLayer({
          source: new OSM()
        })
      ],
      target: this.mapRef.current,
      view: new View({
        center: fromLonLat([lon, lat]),
        zoom: 15
      })
    });

    this.setState({ map });
  }
  render() {
    return <div className="ol-map" ref={this.mapRef} />;
  }
}

export default OpenLayersMap;

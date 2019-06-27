import "ol/ol.css";
import "./OpenLayersMap.css";
import React, { Component } from "react";
import { Feature, Map, View } from "ol";
import OSM from "ol/source/OSM.js";
import VectorSource from "ol/source/Vector";
import TileLayer from "ol/layer/Tile.js";
import { Point } from "ol/geom";
import VectorLayer from "ol/layer/Vector";
import { fromLonLat, toLonLat, transform } from "ol/proj";

export class OpenLayersMap extends Component {
  constructor(props) {
    super(props);
    this.mapRef = React.createRef();
  }
  componentDidUpdate() {
    this.state.map.updateSize();
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

    if (!this.props.readonly) {
      map.on("click", evt => {
        var lonlat = transform(evt.coordinate, "EPSG:3857", "EPSG:4326");
        var coord = toLonLat(evt.coordinate);
        this.reverseGeocode(coord);
        this.makeMarker(lonlat);
      });
    }
    this.setState({ map });
  }
  reverseGeocode = coords => {
    fetch(
      "https://nominatim.openstreetmap.org/reverse?format=json&lon=" +
        coords[0] +
        "&lat=" +
        coords[1]
    )
      .then(response => {
        return response.json();
      })
      .then(json => {
        console.log(json);
        console.log(
          `${json.address.road},${json.address.house_number},${
            json.address.city
          },${json.address.postcode} `
        );
        if (this.props.onClick) {
          this.props.onClick(json);
        }
      });
  };

  makeMarker = lonlat => {
    var marker = new Feature({
      geometry: new Point(fromLonLat(lonlat))
    });

    var vectorSource = new VectorSource({
      features: [marker]
    });

    var markerVectorLayer = new VectorLayer({
      source: vectorSource
    });

    this.state.map.addLayer(markerVectorLayer);
  };

  render() {
    return <div className="ol-map" ref={this.mapRef} />;
  }
}

export default OpenLayersMap;

import "ol/ol.css";
import "./OpenLayersMap.css";
import React, { Component } from "react";
import { Feature, Map, View } from "ol";
import OSM from "ol/source/OSM.js";
import VectorSource from "ol/source/Vector";
import TileLayer from "ol/layer/Tile.js";
import { Point } from "ol/geom";
import VectorLayer from "ol/layer/Vector";
import { Icon, Style } from "ol/style";
import { fromLonLat, toLonLat, transform } from "ol/proj";

export class OpenLayersMap extends Component {
  constructor(props) {
    super(props);
    this.mapRef = React.createRef();
  }
  componentDidUpdate() {
    this.map.updateSize();
  }
  componentDidMount() {
    this.initMap();
  }

  initMap = () => {
    const { lon = 0, lat = 0, markerLon, markerLat } = this.props;
    this.map = new Map({
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

    if (markerLon && markerLat) {
      const lonlat = [markerLon, markerLat];
      this.createOrUpdateMarker(lonlat);
    }

    if (!this.props.readonly) {
      this.map.on("click", evt => {
        const lonlat = transform(evt.coordinate, "EPSG:3857", "EPSG:4326");
        const coord = toLonLat(evt.coordinate);
        this.reverseGeocode(coord);
        this.createOrUpdateMarker(lonlat);
      });
    }
  };

  createOrUpdateMarker = lonlat => {
    if (this.marker) {
      this.marker.setGeometry(new Point(fromLonLat(lonlat)));
      return;
    }
    this.marker = new Feature({
      geometry: new Point(fromLonLat(lonlat))
    });

    this.marker.setStyle(
      new Style({
        image: new Icon({
          src: "images/bighouse.png"
        })
      })
    );

    this.vectorSource = new VectorSource({
      features: [this.marker]
    });

    this.markerVectorLayer = new VectorLayer({
      source: this.vectorSource
    });
    this.map.addLayer(this.markerVectorLayer);
  };

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

  render() {
    return <div className="ol-map" ref={this.mapRef} />;
  }
}

export default OpenLayersMap;

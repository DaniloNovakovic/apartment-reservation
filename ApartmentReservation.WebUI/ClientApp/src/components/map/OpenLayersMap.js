import "ol/ol.css";
import "./OpenLayersMap.css";
import React, { Component } from "react";
import { Feature, Map, View } from "ol";
import OSM from "ol/source/OSM.js";
import VectorSource from "ol/source/Vector";
import { Point } from "ol/geom";
import { Tile as TileLayer, Vector as VectorLayer } from "ol/layer.js";
import { Icon, Style } from "ol/style";
import { fromLonLat, toLonLat, transform } from "ol/proj";
import { click } from "ol/events/condition";
import Select from "ol/interaction/Select.js";

function areMarkersEqual(newMarkers = [], oldMarkers = []) {
  if (newMarkers.length !== oldMarkers.length) {
    return false;
  }
  for (let i = 0; i < newMarkers.length; ++i) {
    if (
      JSON.stringify(newMarkers[i]) !== JSON.stringify(oldMarkers[i].props.id)
    ) {
      return false;
    }
  }
  return true;
}

export class OpenLayersMap extends Component {
  constructor(props) {
    super(props);
    this.mapRef = React.createRef();
  }
  componentDidUpdate(prevProps) {
    console.group("UPDATE OpenLayersMap");

    this.map.updateSize();

    const markers = this.props.markers || [];
    const prevMarkers = prevProps.markers || [];

    if (!areMarkersEqual(markers, prevMarkers)) {
      const layers = this.map.getLayers();
      for (let layer of layers.getArray()) {
        if (layer.type !== "TILE") {
          layer.setVisible(false);
          layer.setOpacity(0);
        }
      }
      this.createMarkers();
    }
    console.groupEnd();
  }

  componentDidMount() {
    this.initMap();
    this.createMarkers();
  }

  initMap = () => {
    const { lon = 0, lat = 0, markerLon, markerLat, zoom = 15 } = this.props;
    this.map = new Map({
      layers: [
        new TileLayer({
          source: new OSM()
        })
      ],
      target: this.mapRef.current,
      view: new View({
        center: fromLonLat([lon, lat]),
        zoom: zoom
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

    this.addEventHandlers();
  };
  createMarkers = () => {
    const { markers = [] } = this.props;
    console.log(markers);
    for (let item of markers) {
      const lonlat = [item.lon, item.lat];
      const marker = new Feature({
        geometry: new Point(fromLonLat(lonlat))
      });
      const src = item.src || "images/bighouse.png";
      console.log(src);
      marker.setStyle(
        new Style({
          image: new Icon({
            src: src
          })
        })
      );
      marker.setProperties({ ...(item.props || {}), name: "marker" });
      const vectorSource = new VectorSource({
        features: [marker]
      });
      const markerVectorLayer = new VectorLayer({
        source: vectorSource
      });
      this.map.addLayer(markerVectorLayer);
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

    const markerProps = this.props.markerProps || {};
    this.marker.setProperties({ ...markerProps, name: "marker" });

    this.vectorSource = new VectorSource({
      features: [this.marker]
    });

    this.markerVectorLayer = new VectorLayer({
      source: this.vectorSource
    });
    this.map.addLayer(this.markerVectorLayer);
  };

  addEventHandlers() {
    var selectClick = new Select({
      condition: click
    });

    this.map.addInteraction(selectClick);

    selectClick.on("select", event => {
      const feature = event.selected[0];
      if (feature && feature.get("name") === "marker") {
        const onClick = feature.get("onClick");
        if (onClick) {
          onClick();
        }
      }
    });
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

import React from "react";
import { Map, Marker, GoogleApiWrapper } from "google-maps-react";
import "./Map.css";

class GoogleMap extends React.Component {
  constructor(props) {
    super(props);
    this.state = {};
  }

  componentDidMount() {
    // this.getGeolocation();
  }
  //   getGeolocation() {
  //     if ("geolocation" in navigator) {
  //       navigator.geolocation.getCurrentPosition(function (position) {
  //         let latitude = position.coords.latitude.toFixed(6);
  //         let longitude = position.coords.longitude.toFixed(6);
  //         return latitude, longitude;
  //       });
  //     } else {
  //       window.alert(
  //         "We are having trouble locating you, did you consent to share your location?"
  //       );
  //     }
  //   }

  render() {
    const style = {
      width: "100%",
      height: "100%",
      position: "relative",
    };
    return (
      <div className="map-contaiter">
        <Map
          google={this.props.google}
          zoom={12}
          initialCenter={{
            lat: 35.5496939,
            lng: -120.7060049,
          }}
          style={style}
          onClick={this.onMapClicked}
        >
          <Marker onClick={this.onMarkerClick} name={"Current location"} />
        </Map>
      </div>
    );
  }
}

export default GoogleApiWrapper({
  apiKey: "AIzaSyAxJgs4IB49EF8hpesrSjimBe5FPa0kytk",
})(GoogleMap);

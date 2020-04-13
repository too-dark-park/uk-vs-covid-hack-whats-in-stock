import React from "react";

export default function Map() {
  //   "https://maps.googleapis.com/maps/api/js?key=AIzaSyAKmBYHO8pHLJzc0zOwWjJ4-v4v1EExi9A";

  return (
    <GoogleMap
      defaultZoom={10}
      defaultCenter={{ lat: 45.421532, lng: -75.697189 }}
    />
  );
}

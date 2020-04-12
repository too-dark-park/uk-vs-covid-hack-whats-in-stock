import React from "react";
import "./App.css";
import Button from "./Components/Button/Button";
import logo from "./logo_final.png";
import Menu from "./Components/Menu/Menu";

class App extends React.Component {
  this.state = {
    data: null,
    latitude: "",
    longitude: "",
    error: null
  }

  async getJson = () => {
    let latitude, longitude;
    if ("geolocation" in navigator) {
      navigator.geolocation.getCurrentPosition(function (position) {
        latitude = position.coords.latitude.toFixed(6);
        longitude = position.coords.longitude.toFixed(6);
        console.log(latitude, longitude);
      });
    } else {
      this.setState({error: "geoLocation API not available - Bhavik sucks"});
      return;
    }
    const linkJson = `https://whatsin.whiscode.dotnetcloud.co.uk/places/nearby?latitude=${latitude}&longitude=${longitude}`;
    const dataJson = await fetch(linkJson, { cache: "no-cache" }).then(res => {
      if (!res.ok) {
        this.setState({error: "unable to fetch data - Bhavik still sucks"});
      }
      return res.json();
    })
    .catch(error => {
      this.setState({ data: null, longitude, latitude, error: `${error} - Bhavik ubersucks` });
    });
    this.setState({ data: dataJson, longitude, latitude, error: null });
    // not sure if it is worth storing the coordinates in state, but there it is
    console.log("link", linkJson);
  }

  componentDidMount() {
    //FYI, binding is unnecessary if you declare method as arrow functions within the scope of the class
    this.getJson();
  }

  componentDidCatch(error, info) {
    this.setState({ error });
  }

  render() {
    const { data, error } = this.state;
    return error ? (
      <div>Ooops... something went wrong: {error}.</div>
    ) : (
      <div className="main-container">
        {data && console.log(this.state.data)}
        <header className="App-header">
          <Menu />
        </header>{" "}
        {/*Menu component*/}
        <section>
          <div className="container">
            <img src={logo} alt="" />
            <p className="blurb">
              No loo roll? <br />
              Long supermarket queue? <br />
              Find it elsewhere.
            </p>
          </div>
          <div className="button-container">
            <Button className="landing-button">submit a product</Button>
            <Button className="landing-button">find a product</Button>
          </div>
        </section>
        <footer>
          <a
            className="App-link"
            href="https://github.com/too-dark-park/uk-vs-covid-hack-whats-in-stock"
            target="_blank"
            rel="noopener noreferrer"
          >
            Link to our GitHub
          </a>
        </footer>
      </div>
    );
  }
}

export default App;

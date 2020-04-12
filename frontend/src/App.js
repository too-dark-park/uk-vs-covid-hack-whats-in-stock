import React from "react";
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';
import "./App.css";
import Button from "./Components/Button/Button";
import logo from "./logo_final.png";
import Menu from "./Components/Menu/Menu";



class App extends React.Component {
  _isMounted = false;

  constructor(props) {
    super(props);

    this.state = {
      data: this.getJson(),
      error: false,
    };
  }

  async getJson() {
    const linkJson =
      "https://whatsin.whiscode.dotnetcloud.co.uk/WeatherForecast";
    const fetchJson = await fetch(linkJson, { cache: "no-cache" });
    const dataJson = await fetchJson.json();
    if (this._isMounted) {
      this.setState({ data: dataJson });
    }
  }

  componentDidMount() {
    this._isMounted = true;
    this.getJson = this.getJson.bind(this);
    this.getJson();
  }

  componentDidCatch(error, info) {
    this.setState({ error: true });
  }

  componentWillUnmount() {
    this._isMounted = false;
  }

  render() {
    const { data, error } = this.state;
    return error ? (
      <div>Ooops... something went wrong.</div>
    ) : (
        <div className="main-container">
          {data && console.log(this.state.data)}

          <header className="App-header"><Menu /></header> {/*Menu component*/}

          <section>
            <div className="container">
              <img src={logo} alt="" />
              <p className="blurb">
                Low on toilet paper but the queues outside the shop are too long?
                Save yourself a trip and find what's in stock before you leave the
                house.
            </p>
            </div>
            <Button>SUBMIT A PRODUCT</Button>
            <Button>FIND A PRODUCT</Button>
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

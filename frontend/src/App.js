import React from "react";
import "./App.css";
import Button from "./Components/Button/Button";
import Menu from "./Components/Menu/Menu";
import { BrowserRouter as Router, Switch, Link, Route } from "react-router-dom";
import FindProduct from "./Components/Pages/FindProduct";
import SubmitProduct from "./Components/Pages/SubmitProduct";
import HomePage from "./Components/Pages/HomePage";
import Logo from "./Components/Logo/Logo";

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
      <Router>
        <div className="main-container">
          {data && console.log(this.state.data)}
          {/*Menu component*/}
          <header className="App-header">
            <Menu />
          </header>{" "}
          {/*Routing*/}
          <section>
            {/* <HomePage /> */}
            <Switch>
              {/* <Route path="/" component={Logo} /> */}
              <Route exact path="/" component={(HomePage, Logo)} />
              <Route exact path="/submit" component={SubmitProduct} />
              <Route exact path="/find" component={FindProduct} />
            </Switch>
            <div className="button-container">
              <Link to="/submit">
                <Button className="landing-button">submit a product</Button>
              </Link>
              <Link to="/find">
                <Button className="landing-button">find a product</Button>
              </Link>
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
      </Router>
    );
  }
}

export default App;

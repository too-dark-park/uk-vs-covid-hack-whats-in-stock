import React from "react";
import "./App.css";
import Button from "./Components/Button/Button";
import logo from "./logo_final.png";
import Menu from "./Components/Menu/Menu";
import { BrowserRouter as Router, Switch, Link, Route } from 'react-router-dom';
import FindProduct from './Pages/FindProduct';
import SubmitProduct from './Pages/SubmitProduct';


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
          <header className="App-header">
            <Menu />
          </header>{" "}
          {/*Menu component*/}
          {/*Home page*/}
          <section>
            <div className="container">
              <img src={logo} alt="" />
              <p className="blurb">
                No loo roll? <br />
              Long supermarket queue? <br />
              Find it elsewhere.
            </p>
            </div>

            {/*Routing*/}
            <Router>
              <div className="button-container">
                <Switch>
                  <Route exact path='/submit' component={SubmitProduct} />
                  <Route exact path='/find' component={FindProduct} />
                </Switch>

                <Link to='/submit'>
                  <Button className="landing-button">submit a product</Button>
                </Link>
                <Link to='find'>
                  <Button className="landing-button">find a product</Button>
                </Link>

              </div>
            </Router>

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

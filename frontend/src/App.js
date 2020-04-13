import React from "react";
import "./App.css";
import Button from "./Components/Button/Button";
// import logo from "./logo_final.png";
import Menu from "./Components/Menu/Menu";
import { BrowserRouter as Router, Switch, Link, Route } from 'react-router-dom';
import FindProduct from './Pages/FindProduct';
import SubmitProduct from './Pages/SubmitProduct';
import HomePage from './Pages/HomePage';

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
          {/*Menu component*/}
          <header className="App-header">
            <Menu />
          </header>{" "}
          {/*Routing*/}
          <section>
            <Router>
              <div className="button-container">
                <Switch>
                  <Route exact path='/' component={HomePage} />
                  <Route exact path='/submit' component={SubmitProduct} />
                  <Route exact path='/find' component={FindProduct} />
                </Switch>

                <Link to='/submit'>
                  <Button className="landing-button">submit a product</Button>
                </Link>
                <Link to='/find'>
                  <Button className="landing-button">find a product</Button>
                </Link>
                <Link to='/'>
                  <Button className="landing-button">go back to the start</Button>
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

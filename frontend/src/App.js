import React from "react";
import "./App.css";

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
      <div className="App">
        {data && console.log(this.state.data)}
        <header className="App-header">
          <h1>WhatsIN</h1>
          <h3>Shopping!</h3>
          <p>
            Low on toilet paper but the queues outside the shop are too long?
            Save yourself a trip and find what's in stock before you leave the
            house.
          </p>
        </header>
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

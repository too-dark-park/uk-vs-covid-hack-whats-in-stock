import React from 'react';
import './Menu.css';
import hamburger from './menu.png';
import App from '../../App';
import ContactUs from './Pages/ContactUs';
import About from './Pages/About';
import { BrowserRouter as Router, Switch, Route } from 'react-router-dom';

export default function Menu() {
  const [menuDisplay, setMenuDisplay] =
    React.useState('menu-display-off'); //refers to CSS

  function toggleMenu() {
    setMenuDisplay(menuDisplay === 'menu-display-off' ?
      '' : 'menu-display-off');
  }

  return (
    <>
      <nav className='menu-closed'>
        <button className='menu-btn' onClick={() => toggleMenu()}>
          <img className='menu-icon' src={hamburger} alt='' />
        </button>
      </nav>

      <Router>
        <main className='menu-open' id={menuDisplay}>
          <Route exact path='/' component={} />
          {/* <h4><a href='/'>Start again</a></h4> */}
          <Route exact path='/about' component={About} />
          {/* <h4><a href='/'>About</a></h4> */}
          <Route exact path='/contactus' component={ContactUs} />
          {/* <h4><a href='/'>Contact Us</a></h4> */}
        </main>
      </Router>
    </>
  );

}


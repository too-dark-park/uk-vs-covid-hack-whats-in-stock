import React from 'react';
import './Menu.css';
import hamburger from './menu.png';

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
        <button className='menu-btn' onClick={() => toggleMenu}>
          <img className='menu-icon' src={hamburger} alt='' />
        </button>
      </nav>

      <main className='menu-open' id={menuDisplay}>
        <h4><a href='/'>Submit a product</a></h4>
        <h4><a href='/'>Find a product</a></h4>
        <h4><a href='/'>About</a></h4>
        <h4><a href='/'>Contact Us</a></h4>
      </main>
    </>
  );

}


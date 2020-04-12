import React from 'react';
import menu_icon from './menu.png';

export default function Menu() {

  return (
    <>
      <nav className='menu-closed'>
        <button className='menu-btn'>
          <img className='menu-icon' src={menu_icon} alt='' />
        </button>
      </nav>

      <main className='menu-open'>
        <h4><a href='/'>Submit a product</a></h4>
        <h4><a href='/'>Find a product</a></h4>
        <h4><a href='/'>About</a></h4>
        <h4><a href='/'>Contact Us</a></h4>
      </main>
    </>
  );

}


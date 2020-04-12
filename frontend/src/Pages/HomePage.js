import React from 'react';
import logo from './logo_final.png';
import '../App.css';

export default function HomePage() {
  return (
    <div className="container">
      <img src={logo} alt="WhatsIn logo" />
      <p className="blurb">
        No loo roll? <br />
        Long supermarket queue? <br />
        Find it elsewhere.
      </p>
    </div>
  );
}

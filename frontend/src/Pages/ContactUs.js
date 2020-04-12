import React from 'react';
import { Link } from 'react-router-dom';

export default function ContactUs() {
    return (
    <Link to='/contactus'>
      <div>Contact Us</div>
      <Link to='/'>Home</Link>
      <Link to='/films'>Films</Link>
      <Link to='/contactus'>Contact Us</Link>
    </Link>
    );
  }

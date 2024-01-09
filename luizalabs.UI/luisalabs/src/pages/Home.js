import React, { useState } from 'react';
import axios from 'axios';

const Home = () => {

    const handleTestAuthRequest = async() => {
        var test = await axios.get(`http://localhost:54971/Users/659c1a2f6a207835e220e22c`);
      };

    setTimeout(() => {
        handleTestAuthRequest();
      }, 3 * 1000);

    return (
        <div>User Home Page</div>
    );
};

export default Home;